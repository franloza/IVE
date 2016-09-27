Second report 08/10 - 4 pages
=============================

Meeting (27/09)
===============

1.  Research questions

-   Is it better in terms of comprehension to visualize networks graphs with/without stereoscopy and head tracking?

-   Dose head-tracking help and stereoscopy increase this comprehension?

-   We can compare with or without OR but we can’t really compare visual memory conclusions with the paper.

-   How much the performance is reduced with a reduced field of vision?

1.  Approach

We don’t reduce the angle of vision for 2D. Just to control versus 3D and stereoscopy vision.

Firstly, the subject will perform the first stage of the experiment without OR with 3 levels of graph complexity.

Secondly, the subject will perform the second stage of the experiment with OR with 3 levels of graph complexity

Finally, the subject will perform the third stage of the experiment with OR and a reduced field of vision with 3 levels of graph complexity.

The results obtained by one single subject will be part of the analysis of the learning, the adaption to the environment and fatigue.

The subject will need to determine if there exists a path of length two or not in the graph.

1.  Metrics

-   Time for completing the task

-   Error rate

-   Distance and head movement

1.  Constraints

-   A maximum amount of time will be defined for each level of complexity. The range of maximum time will be in a range between 10 seconds and 1 minute, but this is an approximation and could be changed after the implementation of the experiment.

-   If the subject runs out of time for one level, the next level or stage will start.

-   The subject won’t be allowed to stand up from the chair. He will be able to rotate his head and move it at any direction though.

-   A subject will be allowed to repeat the experiment as much as possible.

-   The subject will sit in a range of 60 centimeters and 1 meter.

-   If the subject interrupts the experiment without completing all the stages and levels, the results will not be valid.

-   The subject won’t be allowed to take out the OR in the second and the third stage. If the subject takes out the OR during this stages, the experiment will be interrupted and the data collected will be invalid.

-   The subject won’t be provided with any joystick to move inside the scenarios. Only he will have access to the mouse to give the answers for the graphs.

1.  Technical details

-   We will generate the graphs randomly for each level of complexity but according to some constraints. The number of edges and nodes will be defined for each level according to the time it takes to complete and the difficulty to find a path.

-   The graph will be undirected. Two nodes will be highlighted in red and the rest will be in blue.

-   The probability that the two selected nodes for each level will be connected by a path of length two is 50%.

-   We will use Unity framework for the experiment

-   For the third stage, the angle of vision will be reduced around 50%.

-   The subject will use the mouse to answer if there is a 2 length path between the paths or not.

-   The background will be black.

-   No movement inside the scenario will be implemented.
