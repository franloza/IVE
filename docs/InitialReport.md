Interactive Virtual Environments project. Initial report
========================================================

Depth perception: Effects of Head Tracking in Analysing Graphs in 3D
--------------------------------------------------------------------

### Date: September 2016

### Eindhoven University of Technology (TU/e)

1. Group members and roles:
---------------------------

-   Allegretta, Mauro - Coordination, version control and reporting

-   Ambite, Diego - Theory and literature consultant

-   Lozano, Francisco J. - Data analyst

-   Rinsma, Thomas - Computer graphics specialist

2. Problem statement
--------------------

The main goal for this project is to replicate one of the experiments carried out in the provided paper *Evaluating Stereo and Motion Cues for Visualizing Information Nets in Three Dimensions*. This experiment consists of analyzing the difference between user comprehension of network graphs (1) in 2D versus 3D and (2) with and without head-tracking. This difference will be measured by having the user perform a simple task and recording several metrics such as timing and error rate. As a result of the experiment, we hope to be able to make conclusions about whether 3D vision and/or (lack of) head-tracking is better for the comprehension of visualized graphs.

In addition to the metrics proposed in the experiment, we will try to gain more insight of the effects of 3D vision and head-tracking by measuring additional metrics such us the distance traversed or the amount rotated by the head or the distance that the subject traverses inside the virtual environment. The purpose of these additional metrics is to test whether or not there exists a correlation between the complexity of the graph and the amount of movement that is required for the subject to comprehend it.

Finally, we will have to overcome some difficulties that could lead to the failure of the experiment. Some of these difficulties are:

-   Retrieve some metrics from Oculus Rift and get used to the Unity framework.

-   Design appropriate levels (of difficulty) for our analysis purpose in order to get useful data.

-   Interpret the data correctly to extract truthful and correct conclusions.

-   Technical difficulties/limitations regarding frame rate, graphics rendering, etc.

3. Research questions
---------------------

The questions that we will try to give answer to are the following ones:

-   Is it better in terms of comprehension to visualize network graphs in 2D or 3D?

-   Does head-tracking help increase this comprehension in addition to 3D vision?

-   Do these differences become bigger as we increase the complexity of the graph?

-   How similar are the obtained results to those in the original paper?

4. Approach
-----------

The experiment will be developed using the Unity framework and it will reproduce the experiment carried out in the provided paper. A series of 5 graphs for each type of visualization will be presented to the subject. The goal for each graph will be to guess if there exists a path of length two connecting two nodes that will be highlighted, with a 50 % probability of either occurring.

The graphs will increase their complexity in terms of a higher amount of nodes and edges. Specifically, the number of nodes in each graph will be the following one:

20 – 45 – 80 – 100 – 130

Firstly, the subject will guess the graphs displayed in 2D, then in 3D without head-tracking, and finally in 3D with head-tracking.

Several metrics will be collected such as the time spent for giving an answer, the error percentage, the distance traversed inside the game with the keyboard and the distance traversed by the head.

Finally, the subject will be asked to grade the experience from 1 to 10 for each type of visualization.
