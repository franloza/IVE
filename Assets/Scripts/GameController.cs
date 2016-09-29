/*
 * Copyright 2014 Jason Graves (GodLikeMouse/Collaboradev)
 * http://www.collaboradev.com
 *
 * This file is part of Unity - Topology.
 *
 * Unity - Topology is free software: you can redistribute it 
 * and/or modify it under the terms of the GNU General Public 
 * License as published by the Free Software Foundation, either 
 * version 3 of the License, or (at your option) any later version.
 *
 * Unity - Topology is distributed in the hope that it will be useful, 
 * but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License 
 * along with Unity - Topology. If not, see http://www.gnu.org/licenses/.
 */

using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

namespace Topology {

	public class GameController : MonoBehaviour {

        //Maximum time for each level
        private const float MAX_TIME = 10f;

		public Node nodePrefab;
		public Link linkPrefab;

		private Hashtable nodes;
		private Hashtable links;
		private GUIText statusText;
		private int nodeCount = 0;
		private int linkCount = 0;
		private GUIText nodeCountText;
		private GUIText linkCountText;
        private GUIText timerText;

        private Canvas visualReduction;

        //Countdown timer 
        private float timeLeft;

        /*
		//Method for loading the GraphML layout file
		private IEnumerator LoadLayout(){

			string sourceFile = Application.dataPath + "/Data/layout.xml";
			statusText.text = "Loading file: " + sourceFile;

			//determine which platform to load for
			string xml = null;
			if(Application.isWebPlayer){
				WWW www = new WWW (sourceFile);
				yield return www;
				xml = www.text;
			}
			else{
				StreamReader sr = new StreamReader(sourceFile);
				xml = sr.ReadToEnd();
				sr.Close();
			}

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);

			statusText.text = "Loading Topology";

			int scale = 2;

			XmlElement root = xmlDoc.FirstChild as XmlElement;
			for(int i=0; i<root.ChildNodes.Count; i++) {
				XmlElement xmlGraph = root.ChildNodes[i] as XmlElement;

				for(int j=0; j<xmlGraph.ChildNodes.Count; j++) {
					XmlElement xmlNode = xmlGraph.ChildNodes[j] as XmlElement;

					//create nodes
					if(xmlNode.Name == "node"){
						float x = float.Parse(xmlNode.Attributes["x"].Value)/scale;
						float y = float.Parse (xmlNode.Attributes["y"].Value)/scale;
						float z = float.Parse(xmlNode.Attributes["z"].Value)/scale;

						Node nodeObject = Instantiate(nodePrefab, new Vector3(x,y,z), Quaternion.identity) as Node;
						//nodeObject.nodeText.text = xmlNode.Attributes["name"].Value;

						nodeObject.id = xmlNode.Attributes["id"].Value;

                        Color red = new Color(1, 0, 0);
                        if (j % 2 == 0)
                            nodeObject.GetComponent<Renderer>().material.color = red;

						nodes.Add(nodeObject.id, nodeObject);

						statusText.text = "Loading Topology: Node " + nodeObject.id;
						nodeCount++;
						nodeCountText.text = "Nodes: " + nodeCount;
					}

					//create links
					if(xmlNode.Name == "edge"){
						Link linkObject = Instantiate(linkPrefab, new Vector3(0,0,0), Quaternion.identity) as Link;
						linkObject.id = xmlNode.Attributes["id"].Value;
						linkObject.sourceId = xmlNode.Attributes["source"].Value;
						linkObject.targetId = xmlNode.Attributes["target"].Value;
						linkObject.status = xmlNode.Attributes["status"].Value;
						links.Add(linkObject.id, linkObject);

						statusText.text = "Loading Topology: Edge " + linkObject.id;
						linkCount++;
						linkCountText.text = "Edges: " + linkCount;
					}

					//every 100 cycles return control to unity
					if(j % 100 == 0)
						yield return true;
				}
			}

			//map node edges
			MapLinkNodes();

			statusText.text = "";
		}

		//Method for mapping links to nodes
		private void MapLinkNodes(){
			foreach(string key in links.Keys){
				Link link = links[key] as Link;
				link.source = nodes[link.sourceId] as Node;
				link.target = nodes[link.targetId] as Node;
			}
		}
        */

        void GenerateGraph(int numNodes, int minDegree, int maxDegree, float scale)
        {
            nodes.Clear();
            links.Clear();

            for (int i = 0; i < numNodes; ++i)
            {
                Vector3 randPoint = Random.insideUnitSphere * scale;
                Node nodeObject = Instantiate(nodePrefab, randPoint, Quaternion.identity) as Node;

                nodeObject.id = i;

                Color red = new Color(1, 0, 0);
                if (i % 2 == 0)
                    nodeObject.GetComponent<Renderer>().material.color = red;

                nodes.Add(nodeObject.id, nodeObject);
            }

            // Loop over first half of nodes
            for (int i = 0; i < numNodes / 2; ++i)
            {
                int degree = Random.Range(minDegree, maxDegree);
                for (int j = 0; j < degree; ++j)
                {
                    // Connect to randomly to a node from the second half
                    int randomNode = Random.Range(numNodes / 2, numNodes - 1);

                    Link linkObject = Instantiate(linkPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Link;
                    linkObject.id = (i * maxDegree + j);
                    linkObject.source = nodes[i] as Node;
                    linkObject.target = nodes[randomNode] as Node;

                    //print("source: " + (!!linkObject.source) + ", target: " + (!!linkObject.target));

                    links.Add(linkObject.id, linkObject);
                }
            }
        }

        private IEnumerator GenerateTestGraph()
        {
            GenerateGraph(30, 2, 5, 100.0f);

            yield return true;
        }

		void Start () {
			nodes = new Hashtable();
			links = new Hashtable();

			//initial stats
			nodeCountText = GameObject.Find("NodeCount").GetComponent<GUIText>();
			nodeCountText.text = "Nodes: 0";
			linkCountText = GameObject.Find("LinkCount").GetComponent<GUIText>();
			linkCountText.text = "Edges: 0";
			statusText = GameObject.Find("StatusText").GetComponent<GUIText>();
			statusText.text = "";
            visualReduction = GameObject.Find("VisualReduction").GetComponent<Canvas>();
            visualReduction.enabled = false;
            timerText = GameObject.Find("Timer").GetComponent<GUIText>();
            timerText.text = "Time: " + MAX_TIME;
            timeLeft = MAX_TIME;

            StartCoroutine( GenerateTestGraph() );
		}

        void Update()
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + timeLeft.ToString("0.00");

            //Reset the graph if time is over
            if(timeLeft < 0.001) Start();

            //statusText.text = Camera.main.transform.position.ToString();
        }
	}

}
