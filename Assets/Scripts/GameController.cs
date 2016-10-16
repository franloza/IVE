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

        void ClearGraph()
        {
            foreach (Node node in nodes.Values)
            {
                Destroy(node.gameObject);
            }

            foreach (Link link in links.Values)
            {
                Destroy(link.gameObject);
            }

            nodes.Clear();
            links.Clear();
        }

        //TODO: Method to generate new random graph (Reset)
        //TODO: Include complexity (1..3) as parameter
        void GenerateGraph(int numNodes, int minDegree, int maxDegree, float scale)
        {
            ClearGraph();
            

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
