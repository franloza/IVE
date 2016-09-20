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

The main goal for this project is to replicate one of the experiments carried out in the provided paper *Evaluating Stereo and Motion Cues for Visualizing Information Nets in Three Dimensions*. This experiment consists on analysing the difference between visualizing network graphs in 2D and 3D. This difference will be specified in terms of different metrics such as time, error percentage and scores made by the subjects regarding to the difficulty. As a result of the experiment, we will be able to extract conclusions about which type of vision is better for visualizing graphs.

In addition to the metrics proposed in the experiment, we will try to extract additional conclusions of 3D vision by measuring additional metrics such us the distance traversed by the head or the distance that the subject traverse inside the scenario. The purpose of this additional metrics is to verify if there exists a correlation between the complexity of the graph and the amount of movement that is required to visualize in 3D. If this correlation does not exist, we will be able to conclude that 3D does not support the task visualizing complex graphs.

Finally, we will have to overcome some difficulties that would lead to the failure of the experiment. Some of these difficulties are:

-   Retrieve some metrics from Oculus Rift and get used to the Unity framework.

-   Design appropriate levels for our analysis purpose in order to get useful data.

-   Interpret the data correctly to extract truthful and correct conclusions.

-   Technical difficulties/limitations regarding to frame rate, graphics rendering...

3. Research questions
---------------------

The questions that we will try to give answer are the following ones:

-   Is it better to visualize network graphs in 2D or 3D?

-   How much is the difference among these kind of visualization?

-   Do these differences become bigger as we increase the complexity of the graph?

-   How does head tracking helps in 3D visualization beside 2D visualization?

-   How similar are the obtained results in comparison with the paper?

4. Approach
-----------

The experiment will be developed using the Unity framework and it will reproduce the experiment carried out in the provided paper. A series of 5 graphs for each type of visualization will be presented to the subject. The goal for each graph will be to guess if there exists a path of length two connecting two nodes that will be highlighted, with a 50 % of probability of each occurring.

The graphs will increase their complexity in terms of a higher amount of nodes and edges. Specifically, the number of nodes in each graph will be the following one:

20 – 45 – 80 – 100 – 130

Firstly, the subject will guess the graphs displayed in 2D and later in 3D using the Oculus Rift.

Several metrics will be collected such as the time spent for giving an answer, the error percentage, the distance traversed inside the game with the keyboard and the distance traversed by the head. These two last metrics will be collected only for 2D visualization.

Finally, the subject will be asked to grade the experience from 1 to 10 for each type of visualization.
