Interactive Virtual Environments project. Report: Implementation and experiment design.
=======================================================================================

Depth perception: Effects of Head Tracking in Analysing Graphs in 3D
--------------------------------------------------------------------

Allegretta, Mauro

Ambite, Diego

Lozano, Francisco J.

Rinsma, Thomas

### Date: October 2016

### Eindhoven University of Technology (TU/e)

Detailed structure of the experiment
------------------------------------

### General description

The experiment that we are performing has a main purpose to collect relevant and consistent data from the Oculus Rift device and the subjects regarding the effects of stereoscopic vision and head tracking in the task of 3D graph comprehension. In addition, we will create a series of scenarios to retrieve data regarding to the effects of visual memory in 3D graph comprehension when the range of vision is reduced.

This experiment is an extension of the one proposed in the study *Evaluating Stereo and Motion Cues for Visualizing Information Nets in Three Dimensions.* Whereas the effects of head tracking and 3D stereoscopic vision will be compared with the study, we will try to make some conclusions about the effects of visual memory on the performed task.

The experiment will be composed of **three stages**. Each one will be composed of **three scenarios** according to the graph complexity**.** The description of each stage is the following one:

1.  **Stage 1**: The subject performs the experiment in each scenario directly on the screen of a laptop. For this stage, no Oculus Rift device will be used and the subject will be able to visualize the graph looking at the screen, with no head tracking support.

2.  **Stage 2**: The subject performs the experiment in each scenario using the Oculus Rift device. Head tracking will be active and there will not be any kind of reduction of the angle of vision.

3.  **Stage 3**: The subject performs the experiment in each scenario using the Oculus Rift device, Head tracking will be active and there will be a reduction of ~50% of the range of vision both in horizontal and vertical axes.

Each stage will be composed by three scenarios with **challenges** whose level of difficulty goes from 1 to 3. A challenge will consist of answering if there exists a 2-length path connecting two highlighted nodes. Each challenge will have a 50% of probability of containing such a path. The subject will have to answer (yes or not) whether or not his path exists before the maximum time for the challenge is over. If the user has not made a decision before the time is over, it will count as a fail.

The complexity of the graph will be defined in terms of the number of nodes and edges in the graph. Each scenario will contain a graph with different complexity in increasing order. Due to the difficulty to determine the number of nodes and edges that fit best with each level of complexity, some testing will still be performed using different number of nodes and edges to design three complexity levels that have distinct avarage times needed to solve the problem in the experiment.

The estimated mean time to complete the three stages will depend on the maximum time we define for each level of complexity but it will not be more than 10 minutes. The estimated time for each stage will be in a range between 1 and 3 minutes and a mean time of 1 minute between each stage.

Finally, the experiment will be allowed to be repeated several times by a subject. The first attempt will be considered for the main study and the consecutive attempts will be taken into account for a possible secondary study about the adaptive process in each stage.

### Constraints

For this experiment, we have defined some rules and constraints that will delimit the experiment's boundary and specify clearly what the subject can or can't do. The constraints are the following:

-   A maximum amount of time will be defined for each level of complexity. The range of maximum time will be somewhere between 10 seconds and 1 minute. This is an approximation and could be modified after the implementation of the experiment.

-   If the subject runs out of time for one level, the next level or stage will start and the challenge will be treated as failed.

-   The subject won't be allowed to stand up from the chair. He will be able to rotate his head and move it at any direction though.

-   A subject will be allowed to repeat the experiment a maximum of 3 times to avoid virtual reality sickness.

-   The subject will sit in a range between 60 centimetres and 1 meter in front of the laptop screen for Stage 1.

-   If the subject interrupts the experiment without completing all the stages and levels, the results will be discarded.

-   The subject won't be allowed to take out the Oculus Rift in Stages 2 and 3. If the subject takes out the OR during this stages, the experiment will be interrupted and the data collected will be discarded.

-   The subject won't be provided with any joystick to move inside the scenarios. He will have access to the mouse and/or keyboard to provide a yes or no answer to each experiment.

    1.  Research questions
        ------------------

In this section we are defining the different approaches that we will use to answer the research questions with the data collected in the experiment. The proposed research questions are the following ones:

-   *Does stereoscopy and head tracking add to a person's comprehension of 3D graphs?*

This question will be answered using the data collected from Stages 1 and 2. We will compare the difference of performance in terms of time and error rate. In addition, the subject will be asked at the end of the experiment to rate the experience in each stage. Data collected in these two stages will be compared among them to see if these techniques are really helpful and how much are they in terms of performance.

-   *Does there exist any correlation between the amount of head movement and the graph complexity when 3D graphs are visualized with a virtual reality device?*

This question will be answered using the data collected from Stages 2 and 3. We will measure the distance traversed by the head for each scenario and use this metric to see if the distance increases proportionally to the complexity of the graph.

-   *How important is the range of vision in comprehension of 3D graphs with stereoscopic vision? Does visual memory have any influence in the comprehension of 3D graphs?*

The second question is the most relative one because it depends on many factors that are hard to measure without special equipment. However, our approach of reducing the range of vision of the graph could give us some idea of how much the difference in the performance is in this scenario. We hypothesize that the reduction of the range of vision in Stage 3 will make the subject look several times in different directions to construct a memory image of the graph. This probably takes a certain amount of time that we will compare with the results obtained in Stage 2. Analysing the results, we will try to form a conclusion on how significant the use of visual memory is in the comprehension of 3D graphs.

-   *How similar are the obtained results with the findings in the original paper?*

This question will be answered using the data collected from Stages 1 and 2 and once the first question has been answered. We will make a comparison of the conclusions of our experiment and the paper and try to look for the reasons if the results are differences are significant enough.

Software implementation details
-------------------------------

For the implementation of this experiment, we will use Unity framework and Oculus Rift SDK. A controller class will be in charge of controlling the experiment flow across the different stages and scenarios. The controller will contain an internal timer that will control the remaining time for each scenario. The set of rules, such as maximum time and reduction of range of vision, used for each stage will be stored in a data structure.

The only interaction allowed with each scenario will be provided through the mouse. The subject will use the left mouse button to answer that the there exists a 2-length path between the highlighted nodes and the right mouse to give a negative answer. The rest of the interactions such as initiating the timer, change the scenarios or reduce the angle of vision will be than through the controller. No movement will be implemented inside the scenario.

The graphs will be randomly generated for each scenario using some defined constants according to the level of complexity of the graph. Some of this constants will be the number of edges and nodes and the probability of generating a 2-length path between two points. The graph will be undirected, with white edges connecting blue nodes. The highlighted points will be drawn in red and the background of the scenario will be black to improve the visibility of the graph.
