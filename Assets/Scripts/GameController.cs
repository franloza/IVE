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

        // Destroys gameobjects and clears the representation of the graph (hashtables)
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

        // Returns whether or not there is a 2-length path between the highlighted nodes
        bool GenerateGraph(int numNodes, int minDegree, int maxDegree, float scale)
        {
            ClearGraph();

            // Make a 2-length path or not? 50% chance of either
            bool path = UnityEngine.Random.Range(0, 2) == 1;

            // Generate random node positions
            for (int i = 0; i < numNodes; ++i)
            {
                Vector3 randPoint = UnityEngine.Random.insideUnitSphere * scale;
                Node nodeObject = Instantiate(nodePrefab, randPoint, Quaternion.identity) as Node;

                nodeObject.id = i;

                nodes.Add(nodeObject.id, nodeObject);
            }

            // Node highlight color: red
            Color highlight = new Color(1, 0, 0);

            // Keep track of the nodes that are connected
            bool[] connected = new bool[numNodes / 2];

            // Loop over first half of nodes
            for (int i = 0; i < numNodes / 2; ++i)
            {
                // Generate 2-length path
                if (i == 0 && path)
                {
                    // Connect from node 0 to node 1
                    Link linkObject = Instantiate(linkPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Link;
                    linkObject.id = (numNodes * maxDegree + 1);
                    linkObject.source = nodes[0] as Node;
                    linkObject.target = nodes[1] as Node;

                    // And from node 1 to node (numNodes / 2)
                    Link linkObject2 = Instantiate(linkPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Link;
                    linkObject2.id = (numNodes * maxDegree + 2);
                    linkObject2.source = nodes[1] as Node;
                    linkObject2.target = nodes[numNodes / 2] as Node;

                    // Add the links
                    links.Add(linkObject.id, linkObject);
                    links.Add(linkObject2.id, linkObject2);

                    // Highlight node 0 and node (numNodes / 2)
                    ((Node)nodes[0]).GetComponent<Renderer>().material.color = highlight;
                    ((Node)nodes[numNodes / 2]).GetComponent<Renderer>().material.color = highlight;
                }

                int degree = UnityEngine.Random.Range(minDegree, maxDegree);
                for (int j = 0; j < degree; ++j)
                {
                    // Connect to randomly to a node from the second half (minus (numNodes / 2))
                    int randomNode = UnityEngine.Random.Range(numNodes / 2, numNodes - 1);

                    connected[randomNode - numNodes / 2] = true;

                    // Connect node i and randomNode
                    Link linkObject = Instantiate(linkPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Link;
                    linkObject.id = (i * maxDegree + j);
                    linkObject.source = nodes[i] as Node;
                    linkObject.target = nodes[randomNode] as Node;

                    // Add link
                    links.Add(linkObject.id, linkObject);
                }
            }

            // Remove unconnected vertices
            for (int i = 0; i < numNodes / 2; ++i)
            {
                if (!connected[i])
                {
                    Destroy((nodes[i + numNodes / 2] as Node).gameObject);
                    nodes.Remove(i + numNodes / 2);
                }
            }

            // If we don't want to create a 2-length path:
            // Highlight a node from the first half, and one from the second half
            // there will never be a 2-length path between them, because
            // there is never a path from a node to another node in the same half
            if (!path)
            {
                // Highlight node 0 (first half) and node (numNodes / 2) (second half)
                ((Node)nodes[0]).GetComponent<Renderer>().material.color = highlight;
                ((Node)nodes[numNodes / 2]).GetComponent<Renderer>().material.color = highlight;
            }

            return path;
        }

        // Returns whether or not a 2-length path exists between highlighted nodes
        private bool GenerateExperimenGraph(int complexity)
        {
            int numNodes, minDegree, maxDegree;

            switch (complexity)
            {
                case 1:
                    numNodes = 20;
                    minDegree = 2;
                    maxDegree = 3;
                    break;

                case 2:
                    numNodes = 25;
                    minDegree = 2;
                    maxDegree = 4;
                    break;

                case 3:
                    numNodes = 30;
                    minDegree = 2;
                    maxDegree = 5;
                    break;

                default:
                    throw new ArgumentException();
            }

            float scale = 200.0f;

            return GenerateGraph(numNodes, minDegree, maxDegree, scale);
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
            if (exp == null) return;

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
        public void onStageChange(float timeEmployed, float headMovement)
        {
            ClearGraph();
            statusText.text = "Stage " + exp.Stage;
            instructions.text = "";
            warpKeys.text = "";
        }

        public void onChallengeChange(float timeEmployed, float headMovement)
        {
            warpKeys.text = "Stage " + exp.Stage + " - Challenge " + exp.Challenge;
            timerText.text = "Time: " + exp.TimeLeft.ToString("0.00");
            bool path = GenerateExperimenGraph(exp.Challenge);
            //statusText.text = "path: " + (path ? "yes" : "no");
            //Set the solution
            exp.Solution = path;
        }

        public void onFinish(float timeEmployed, float headMovement)
        {
            ClearGraph();
            nodeCountText.text = "";
            linkCountText.text = "";
            statusText.text = "Experiment finished\nCorrect answers: " + exp.NumCorrect + "/9.";
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
            bool path = GenerateExperimenGraph(exp.Challenge);
            //statusText.text = "path: " + (path ? "yes" : "no");
            //Set the solution
            exp.Solution = path;
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
