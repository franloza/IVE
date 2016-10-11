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
using Assets.Scripts.Experiment;
using System.Xml;
using System.IO;
using System;
using System.Text;

namespace Topology {

    public class GameController : MonoBehaviour, ExperimentObserver
    {

        public Experiment exp;

        public Node nodePrefab;
        public Link linkPrefab;

        private Hashtable nodes;
        private Hashtable links;
        private int nodeCount = 0;
        private int linkCount = 0;

        private GUIText statusText;
        private GUIText nodeCountText;
        private GUIText linkCountText;
        private GUIText timerText;
        private GUIText instructions;
        private GUIText warpKeys;
        private Canvas visualReduction;



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

        //TODO: Method to generate new random graph (Reset)
        //TODO: Include complexity (1..3) as parameter
        void GenerateGraph(int numNodes, int minDegree, int maxDegree, float scale)
        {
            nodes.Clear();
            links.Clear();

            for (int i = 0; i < numNodes; ++i)
            {
                Vector3 randPoint = UnityEngine.Random.insideUnitSphere * scale;
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
                int degree = UnityEngine.Random.Range(minDegree, maxDegree);
                for (int j = 0; j < degree; ++j)
                {
                    // Connect to randomly to a node from the second half
                    int randomNode = UnityEngine.Random.Range(numNodes / 2, numNodes - 1);

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

        void Start()
        {
            //Initiate GUI and components
            nodes = new Hashtable();
            links = new Hashtable();
            exp = new Experiment();
            nodeCountText = GameObject.Find("NodeCount").GetComponent<GUIText>();
            linkCountText = GameObject.Find("LinkCount").GetComponent<GUIText>();
            statusText = GameObject.Find("StatusText").GetComponent<GUIText>();
            visualReduction = GameObject.Find("VisualReduction").GetComponent<Canvas>();
            instructions = GameObject.Find("Instructions").GetComponent<GUIText>();
            warpKeys = GameObject.Find("WarpKeys").GetComponent<GUIText>();
            timerText = GameObject.Find("Timer").GetComponent<GUIText>();

            //Subscribe to the experiment
            exp.subscribe(this);
        }

        void Update()
        {
            //Left click - Positive answer
            if (Input.GetMouseButtonDown(0) && !exp.Paused) exp.answer(true);
            //Right click - Negative answer
            else if (Input.GetMouseButtonDown(1) && !exp.Paused) exp.answer(false);

            else if (Input.GetKeyDown("space"))
            {
                if (exp.Finished) exp.reset();
                else if (exp.Paused) exp.start();
            }

            exp.update();
        }

        //Events
        public void onStageChange(float timeEmployed, float headMovement, bool correctAnswer)
        {
            statusText.text = "Stage " + exp.Stage;
            instructions.text = "";
            warpKeys.text = "";
        }

        public void onChallengeChange(float timeEmployed, float headMovement, bool correctAnswer)
        {
            warpKeys.text = "Stage " + exp.Stage + " - Challenge " + exp.Challenge;
            timerText.text = "Time: " + exp.TimeLeft.ToString("0.00");
            StartCoroutine(GenerateTestGraph());
        }

        public void onFinish(float timeEmployed, float headMovement, bool correctAnswer)
        {
            nodeCountText.text = "";
            linkCountText.text = "";
            statusText.text = "Experiment finished";
            visualReduction.enabled = exp.VisualReduction ? true : false;
            timerText.text = "";
            warpKeys.text = "";
            instructions.text = "Press space to start new experiment";
        }

        public void onStart()
        {
            int complexity = exp.Challenge;
            statusText.text = "";
            warpKeys.text = "Stage " + exp.Stage + " - Challenge " + exp.Challenge;
            instructions.text = "";
            visualReduction.enabled = exp.VisualReduction ? true : false;
            //Creates the graph
            //TODO: Add complexity. Graph complexity == Challenge number
            StartCoroutine(GenerateTestGraph());
        }

        public void onReset()
        {
            //Reset the GUI
            nodeCountText.text = "";
            linkCountText.text = "";
            statusText.text = "Graph Visualization Experiment";
            visualReduction.enabled = exp.VisualReduction ? true : false;
            timerText.text = "Time: " + exp.TimeLeft.ToString("0.00");
            warpKeys.text = "";
            instructions.text = "Press space to start";
        }

        public void onUpdate()
        {
            timerText.text = "Time: " + exp.TimeLeft.ToString("0.00");
        }

        public void onSubscribe()
        {
            onReset();
        }

        public void onUnsubscribe()
        {
            throw new NotImplementedException();
        }

        public void onAnswer()
        {
            throw new NotImplementedException();
        }
    }

}
