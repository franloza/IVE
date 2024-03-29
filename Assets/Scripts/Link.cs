﻿/*
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
using System;

namespace Topology {

	public class Link : MonoBehaviour {

		public int id;
		public Node source;
		public Node target;

        public bool loaded = false;

		private LineRenderer lineRenderer;

		void Start () {
            lineRenderer = gameObject.GetComponent<LineRenderer>();

			// Properties
			Color c = Color.white;
			c.a = 1.0f;
            float w = 1;

            //draw line
            //lineRenderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
		    //lineRenderer.material.SetColor ("_Color", c);
			lineRenderer.SetWidth(w, w);
			lineRenderer.SetVertexCount(2);
			lineRenderer.SetPosition(0, new Vector3(0,0,0));
			lineRenderer.SetPosition(1, new Vector3(1,0,0));
		}

		void Update () {
            if (!loaded)
            {
                //print("Loading edge " + id + ", source: " + (!!source) + ", target : " + (!!target) + ", linerenderer = " + (!!lineRenderer));
                lineRenderer.SetPosition(0, source.transform.position);
                lineRenderer.SetPosition(1, target.transform.position);

                loaded = true;
            }
		}
    }

}