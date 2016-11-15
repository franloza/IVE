Interactive Virtual Environments project. Final Report
======================================================

Depth perception: Effects of Head Tracking in Analysing Graphs in 3D
--------------------------------------------------------------------

Allegretta, Mauro

Ambite, Diego

Lozano, Francisco J.

Rinsma, Thomas

### Date: November 2016

### Eindhoven University of Technology (TU/e)

Introduction
------------

Many research about Virtual Reality has proved that the combination of perspective stereoscopic view and tracking the position in real time of the user as he moves is a good idea to visualizing 3D scenes. One of the papers that has gone deeper in the topic of visualizing 3D objects using VR is *Evaluating Stereo and Motion Cues for Visualizing Information Nets in Three Dimensions \[1\]*.

The main goal for this project is to replicate one of the experiments carried out in the mentioned paper. This experiment consists of analyzing the difference between user comprehension of network graphs (1) in 2D versus 3D and (2) with and without head-tracking. This difference will be measured by having the user perform a simple task and recording several metrics such as timing and error rate. As a result of the experiment, we hope to be able to make conclusions about whether 3D vision and/or (lack of) head-tracking is better for the comprehension of visualized graphs.

In addition to the metrics proposed in the experiment, we will try to gain more insight of the effects of 3D vision and head-tracking by measuring additional metrics such us the distance traversed or the amount rotated by the head or the distance that the subject traverses inside the virtual environment. The purpose of these additional metrics is to test whether or not there exists a correlation between the complexity of the graph and the amount of movement that is required for the subject to comprehend it. Of course, our purpose is not to provide a completely general answer about how much is gained by using head-tracking because its use is task specific. The conclusions will only try to generalize a hypothetic application in real life for visualizing graphs and they should not be considered as applicable in all the scenarios where head-tracking is used.

As students, we are conscious that many problems may appear during the development of the project and we will have to overcome some difficulties that could lead to the failure of the experiment. Some of these difficulties are:

-   Retrieve some metrics from Oculus Rift and get used to the Unity framework.

-   Design appropriate levels (of difficulty) for our analysis purpose in order to get useful data.

-   Interpret the data correctly to extract truthful and correct conclusions.

-   Technical difficulties/limitations regarding frame rate, graphics rendering, etc.

All these risks are taken into account and the priority of this study is not only to understand some of the topics we have seen in class but also see the difficulties that imply to deal with a Virtual Reality experiment.

To define a guideline for our experiment, we have defined a series of questions that we will try to give answer:

-   Is it better in terms of comprehension to visualize network graphs in 2D or 3D?

-   Does head-tracking help increase this comprehension in addition to 3D vision?

-   Do these differences become bigger as we increase the complexity of the graph?

-   How similar are the obtained results to those in the original paper?

Therefore, we will try to provide quantitative measures that could prove how much more a graph can be understood in 3D with respect to 2D. At the end of the report, we should give strong reasons about if using 3D graphics and virtual reality is a good combination to interact with information structures.

2. Related work
---------------

As we have mentioned in the previous section, this experiment is an extension of the one proposed in the study *Evaluating Stereo and Motion Cues for Visualizing Information Nets in Three Dimensions.* Despite the paper of Ware and Franck is a good reference for comparing our conclusions with theirs, we must consider the context and the technology used for doing their experiment. This paper was published in 1996, when virtual reality started to be used in some applications but the technology was not very advanced in this area and a lot of research tried to show the possible future applications it may have in the next decade. The hardware used in the paper consisted of 3D LCD shutter glasses that provided the stereo vision and an ultrasonic head-tracking device embedded inside the frame of the glasses. The monitors used for the experiment ran at 120Hz and each eye received a 60Hz update rate using the shutter glasses.

Of course, a great advanced in technology has happened since 1996, and twenty years later, more sophisticated technology is used, and this is an important factor that we cannot overlook. For our experiment, we use Oculus Rift Development Kit 2. This device has a resolution of 960x1080 per eye, a low-persistence OLED display, positional tracking and higher refresh rate that previous versions of Oculus Rift (Up to 75 Hz) and an external HDMI port \[2\].

The difference in technology is one of the main differences between our experiment and Ware and Franck’s experiment. We introduced some differences in the conditions and the variables of the experiment. In the original paper, four different combinations in the type of visualization are carried out: 2D, 2D and head tracking, 3D and 3D and head tracking. However, in our experiment, only three conditions are tried: 2D, 3D and head tracking, and a combination of 3D, head tracking and partial visual reduction. This last condition could not be performed due to technical issues with the development framework and the time constraints of the experiment.

Moreover, the variables taken into account for performing the experiment also changed. In the original paper, the number of nodes are varied to measure the error rate and the time employed by the subjects. They extract conclusions about performance using this metrics. On the other hand, we used a different approach for obtaining different results instead of the raw number of nodes and an abstraction called **complexity level** is used instead. This abstraction is made taken into account the number of nodes and the minimum and maximum degree that they may can have. This degree may vary among the define range but the number of nodes is fixed for each complexity level. Using different types of complexity levels, we tried to simulate different scenarios of complexity and how much does it affect to the error rate and employed time to comprehend the graph.

Finally, the head movement is also taken into account. We use a distance metric provided by the Unity framework in a virtual space that allows us to compare among different amounts of movement. This variable is new respect of the original paper and it’s interesting to analyze because in may provide information about how head-tracking affects the behavior of the subjects and extract conclusions about it.

3. Experiment
-------------

### 3.1. General description

The experiment has been design with the main purpose to collect relevant and consistent data from the Oculus Rift device and the subjects regarding the effects of stereoscopic vision and head tracking in the task of 3D graph comprehension. In addition, we will create a series of scenarios to retrieve data regarding to the effects of visual memory in 3D graph comprehension when the range of vision is reduced.

The experiment will be composed of **three stages**. Each one will be composed of **three scenarios** according to the graph complexity**.** The description of each stage is the following one:

1.  **Stage 1**: The subject performs the experiment in each scenario directly on the screen of a laptop. For this stage, no Oculus Rift device will be used and the subject will be able to visualize the graph looking at the screen, with no head tracking support.

2.  **Stage 2**: The subject performs the experiment in each scenario using the Oculus Rift device. Head tracking will be active and there will not be any kind of reduction of the angle of vision.

3.  **Stage 3**: The subject performs the experiment in each scenario using the Oculus Rift device, Head tracking will be active and there will be a reduction of ~50% of the range of vision both in horizontal and vertical axes.

Each stage will be composed by three scenarios with **challenges** whose level of difficulty goes from 1 to 3. A challenge will consist of answering if there exists a 2-length path connecting two highlighted nodes. Each challenge will have a 50% of probability of containing such a path. The subject will have to answer (yes or not) whether or not this path exists before the maximum time for the challenge is over. If the user has not made a decision before the time is over, it will count as a fail.

The complexity of the graph will be defined in terms of the number of nodes and edges in the graph. Each scenario will contain a graph with different complexity in increasing order. Due to the difficulty to determine the number of nodes and edges that fit best with each level of complexity, some testing will still be performed using different number of nodes and edges to design three complexity levels that have distinct average times needed to solve the problem in the experiment.

The estimated mean time to complete the three stages will depend on the maximum time we define for each level of complexity but it will not be more than 10 minutes. The estimated time for each stage will be in a range between 1 and 3 minutes and a mean time of 1 minute between each stage.

Finally, the experiment will be allowed to be repeated several times by a subject. The first attempt will be considered for the main study and the consecutive attempts will be taken into account for a possible secondary study about the adaptive process in each stage.

### 3.2. Constraints

For this experiment, we have defined some rules and constraints that will delimit the experiment's boundary and specify clearly what the subject can or can't do. The constraints are the following:

-   A maximum amount of time will be defined for each level of complexity. The range of maximum time will be somewhere between 10 seconds and 1 minute. This is an approximation and could be modified after the implementation of the experiment.

-   If the subject runs out of time for one level, the next level or stage will start and the challenge will be treated as failed.

-   The subject won't be allowed to stand up from the chair. He will be able to rotate his head and move it at any direction though.

-   A subject will be allowed to repeat the experiment a maximum of 3 times to avoid virtual reality sickness.

-   The subject will sit in a range between 60 centimeters and 1 meter in front of the laptop screen for Stage 1.

-   If the subject interrupts the experiment without completing all the stages and levels, the results will be discarded.

-   The subject won't be allowed to take out the Oculus Rift in Stages 2 and 3. If the subject takes out the OR during this stages, the experiment will be interrupted and the data collected will be discarded.

-   The subject won't be provided with any joystick to move inside the scenarios. He will have access to the mouse and/or keyboard to provide a yes or no answer to each experiment.

### 3.3. Research questions

In this section we are defining the different approaches that we will use to answer the research questions with the data collected in the experiment. The proposed research questions are the following ones:

-   *Does stereoscopy and head tracking add to a person's comprehension of 3D graphs?*

This question will be answered using the data collected from Stages 1 and 2. We will compare the difference of performance in terms of time and error rate. In addition, the subject will be asked at the end of the experiment to rate the experience in each stage. Data collected in these two stages will be compared among them to see if these techniques are really helpful and how much are they in terms of performance.

-   *Does there exist any correlation between the amount of head movement and the graph complexity when 3D graphs are visualized with a virtual reality device?*

This question will be answered using the data collected from Stages 2 and 3. We will measure the distance traversed by the head for each scenario and use this metric to see if the distance increases proportionally to the complexity of the graph.

-   *How important is the range of vision in comprehension of 3D graphs with stereoscopic vision? Does visual memory have any influence in the comprehension of 3D graphs?*

The second question is the most relative one because it depends on many factors that are hard to measure without special equipment. However, our approach of reducing the range of vision of the graph could give us some idea of how much the difference in the performance is in this scenario. We hypothesize that the reduction of the range of vision in Stage 3 will make the subject look several times in different directions to construct a memory image of the graph. This probably takes a certain amount of time that we will compare with the results obtained in Stage 2. Analysing the results, we will try to form a conclusion on how significant the use of visual memory is in the comprehension of 3D graphs.

-   *How similar are the obtained results with the findings in the original paper?*

This question will be answered using the data collected from Stages 1 and 2 and once the first question has been answered. We will make a comparison of the conclusions of our experiment and the paper and try to look for the reasons if the results are differences are significant enough.

Results
-------

The experiment was performed 16 times with 6 different people. The results were analyzed taken into account the previous descripted metrics and from different perspectives that are reflected in the following 5 plots that are commented individually.

&lt;&lt;PLOT 1&gt;&gt;

The first plot shows how the X axis is split up in three complexity levels and the Y axis measures the average error rate in each complexity level. The first interesting thing that can be observed in this plot is that in the first and third complexity levels, the error rate produced with no use of VR is higher than the error rate when the subjects used VR to visualize the graph. This was expected because the use of VR should make easier the task of visualizing graphs. However, in the second complexity level happens exactly the opposite. This fact was completely unexpected and it’s difficult to make a conclusion about this distortion.

If we take the mean of the percentage of correct answers, that is the inverse of the error rate, the percentage that corresponds to the use of VR is 68.89%. This is slightly higher than the percentage of correct answers without the use of VR, that is 60.47%. This is not a big difference in performance, but follows the tendency proposed in the paper.

<span id="transcription-of-the-thomas-talk-last-pr" class="anchor"></span>On the other hand, independently if we use VR or not, the error rate in the first complexity level is rather higher than the other two complexity levels. This means the opposite of what we expected and what we found in the original paper. That’s it, the error rate should increase proportionally to the level of complexity. The explanation we give for this is that the subjects that visualize the graphs for the first time must get used to the device and the experiment, and this learning process takes some time that we didn’t provide. Therefore, we could say that this was an issue caused by the lack of familiarity with the device and the experiment.

&lt;&lt;PLOT 2&gt;&gt;

In this plot, we can see the amount of head movement produced every time a subject provided either an incorrect or a correct answer. This relation wasn’t discussed in the original paper and it provided some interesting results. The main result was that the amount of head movement increased a 30.19% for correct answer with respect to incorrect answer. That’s it, people that moves their head more to visualize the graph are more likely to do a correct answer. The head movement was measured using the movement of the camera in the virtual space in Unity units. It doesn’t provide a reference to see how is the real distance traversed by the head in meters, but it allows us to compare among different scenarios.

&lt;&lt;PLOT 3&gt;&gt;

Similarly, to the previous plot. we have again the head movement in the Y axis and the X axis is split up in three complexity levels. Specially for the third complexity level, the mean amount of head movement is slightly higher than the others. This was expected because the experiment was set up such that people in higher complexity levels have a higher maximum amount of time and thus, they tend to expend more time and it results in a higher amount of head movement in total. It can be seen also that the variance is very high in the third complexity level due presumably to the fact that people really liked the sensation at this point of the experiment and they moved their heads to take advantage of the experience.

&lt;&lt;PLOT 5&gt;&gt;

In this scatter plot we can observe the same pattern of the previous plot. Almost all the points are clustered in a range among 0 and 10 seconds and almost all of them has an upper bound of 100 units of head movements. From this point, the variance starts to grow exponentially as the number of time increases. This reaffirm the conclusion that when people have more time to give an answer, they try to see the graph from a different perspective to be sure that they are providing a correct answer. Therefore. in scenarios where time is not a constraint to visualize the graph, people uses more head tracking to comprehend better the graph.

&lt;&lt;PLOT 4&gt;&gt;

In these two box plots, we can see the response time in seconds in the Y axis, separated firstly in VR and no VR and secondly in the three complexity levels. We can see for both plots that the higher the graph complexity is, the is higher is the time a subject takes to answer. This was of course expected because as the maximum time to answer increases, people feel less pressure to make a decision. Therefore, we can’t be sure if the graph is really comprehended better or it’s just a matter of it depends on the maximum amount of time provided to answer. This should be compared with another version of the experiment in which the information about the maximum amount of time is not provided and see the real evolution.

We can also see that the variance in the response time is very high again when VR is used. Again, we think that this is caused because people really liked the VR experience and some of them spent higher amounts of time. However, if we observe the means and we compare them, we can observe they are both practically the same in VR (7.81 seconds) and No VR (7.88 seconds). Comparing to the paper, this is not what we expected as the response time should be rather lower when VR is used.

In all the plots we have analyzed, we can observer a common problem: The sample size is too low to make significant conclusions. Several technical problems and constraints in the availability of the device led us to a partial failure in the data collecting. Some of the conclusions we have made follows the tendency of the ones made in the provided paper, but we can’t give our results the same trustworthiness that the one in the paper.

For future work, if this experiment would have repeated, we would change some of the parameters that influences on the results like giving information to the user about the maximum amount of time to answer. In addition, we would try to collect a significant amount of data to get significant and reliable results and compare them with the paper at the same level of trustworthiness.

Conclusions
-----------

This experiment led us to face several problems and to learn the right approach to analyze a research purpose. The first conclusion that we draw is that the actual results are rather more different from the ones we expected. We can explain this due to several factors that depends both on the experiment constraints and the behavior of the subjects. Analyzing the results statistically, we assert that the distortion from our expections could be due to the small size of our dataset. This issue could not be foreseen at the beginning of the experiment because of the uncertainties on the ideal set of subjects, but it turned to be a problem at the end.

The second conclusion is that Virtual Reality offers as many solutions as challenges to face. Dealing with the deadline for this experiment was very challenging because of the technical issues related to the compatibility of the Oculus SDK and the Unity framework. This compelled us to submit the test to a little number of people and limited our capacity to extract significant conclusions from the dataset.

Moreover, the limited time affected the behaviors of the subjects. Some of them misenderstood the task to accomplish, influencing negatively the results or forcing us to discard the tests. On the other hand the subject that was able to accoplish the tasks increased his performances with the growth of the difficulty. This was an unpredictable training process that we didn’t take into account. The second unexpected process was the tendency of the subjects to expend more time to enjoy the VR experience, and not because of it helped in the comprehension of the graph, producing some distortions on the data

Despite the difficulties, we have derived two conclusions from the data: VR and 3D vision with headtracking affects positively the visualizations of 3D graphs and subjects prefer immersive environment and within it they obtain better performances. Thanks to the problems we faced, we spreaded our knowledge not only about software development (Unity developing, Oculus Rift Hardware, Computer Graphics basics etc.) but especially about science (scientifically approach, experimental issues, recuiting subject, evaluating other experiments, being subjects of other groups etc.).

Finally we thought about some possible applications of this results in case they are supported by a bigger number of data and a more solid scientific approach. This kind of experiment could be implemented in the diagnosis and the treatment of neurogical deseases and in the rehab processes measuring the level of the interactions of the patients and their improvements during the period. In this scenario would be also useful the third stage of our experiments that would try to analyze the effect on the visual memory of a restricted field of view.

Appendix. Implementation
------------------------

For the implementation of this experiment, we have used Unity framework and Oculus Rift SDK. A controller class was in charge of controlling the experiment flow across the different stages and scenarios. The controller contained an internal timer that controlled the remaining time for each scenario. The set of rules, such as maximum time and reduction of range of vision, used for each stage are stored in a data structure.

The only interaction allowed with each scenario was provided through the mouse. The subject will use the left mouse button to answer that the there exists a 2-length path between the highlighted nodes and the right mouse to give a negative answer. The rest of the interactions such as initiating the timer, change the scenarios or reduce the angle of vision is done through the controller and no movement is implemented inside the scenario.

The graphs were randomly generated for each scenario using some defined constants according to the level of complexity of the graph. Some of this constants were the number of edges and nodes and the probability of generating a 2-length path between two points. The graph was undirected, with white edges connecting blue nodes. The highlighted points were drawn in red and the background of the scenario will be black to improve the visibility of the graph. Finally, all the results collected were exported to a CSV file and analyzed using Python and matplotlib library.

References
----------

\[1\] Ware, C., & Franck, G. (1996). Evaluating stereo and motion cues for visualizing information nets in three dimensions. ACM Transactions on Graphics, 15(2)

\[2\] Oculus Rift. (n.d.). Retrieved November 2, 2016, from https://en.wikipedia.org/wiki/Oculus\_Rift
