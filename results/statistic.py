import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
# Read in the airports data.
results = pd.read_csv("results2.csv", header=0)
novr = results.loc[results['Stage'] == 1]
vr = results.loc[results['Stage'] == 2] 
vr = vr.drop('Stage',axis=1) 
novr = novr.drop('Stage',axis=1) 

# Error rate Non-VR vs VR
#   No VR
print ("Error rate Non-VR vs VR")
print ('=' * 40)
percentage = novr['Correct Answer'].loc[novr['Correct Answer'] == 1].count() / novr['Correct Answer'].count() * 100
error = novr['Correct Answer'].loc[novr['Correct Answer'] == 0].count() / novr['Correct Answer'].count()
print ("Percentage of correct anwsers without VR: {0:.2f}%".format(percentage))
print ("Error rate without VR: {0:.2f}".format(error))
#   VR
percentage = vr['Correct Answer'].loc[vr['Correct Answer'] == 1].count() / vr['Correct Answer'].count() * 100
error = vr['Correct Answer'].loc[vr['Correct Answer'] == 0].count() / vr['Correct Answer'].count()
print ("Percentage of correct anwsers with VR: {0:.2f}%".format(percentage))
print ("Error rate with VR: {0:.2f}\n".format(error))

# Plot error rate evolution according to complexity
df = pd.DataFrame(np.nan, index=['Challenge 1','Challenge 2','Challenge 3'], columns=['No VR','VR'])
for i in range (1,4):
    errorNVR = novr['Correct Answer'].loc[(novr['Correct Answer'] == 0) & (novr['Challenge'] == i)].count() / novr['Correct Answer'].loc[novr['Challenge'] == i].count()
    errorVR = vr['Correct Answer'].loc[(vr['Correct Answer'] == 0) & (vr['Challenge'] == i)].count() / vr['Correct Answer'].loc[vr['Challenge'] == i].count()
    df.xs('Challenge ' + str(i))[:] = [errorNVR,errorVR]
#fig = df.plot.bar()
#fig.set_ylabel("Error rate") 
#fig.set_title("Error rate per challenge") 

# Mean time for answering Non-VR vs VR per challenge and total
#   No VR
print ("Mean time for answering Non-VR vs VR")
print ('=' * 40)
mean = novr['Time'].mean()
print ("Mean time for answering without VR: {0:.4f}".format(mean))
df = pd.DataFrame()
for i in range (1,4):
    column = novr['Time'].loc[novr['Challenge']==i].reset_index()
    column = column['Time']
    print ("\t* Complexity "+ str(i) + ": {0:.4f}".format(column.mean()))
    df ["Compl. "+str(i)] = column
df["VR Support"] = 'No VR'

#   VR
mean = vr['Time'].mean()
print ("Mean time for answering with VR: {0:.4f}".format(mean))
df2 = pd.DataFrame()
for i in range (1,4):
    column = vr['Time'].loc[vr['Challenge']==i].reset_index()
    column = column['Time']
    print ("\t* Complexity "+ str(i) + ": {0:.4f}".format(column.mean()))
    df2 ["Compl. "+str(i)] = column
df2["VR Support"] = 'VR'
df = df.append(df2)
axes = df.groupby('VR Support').boxplot(return_type='axes')
for i in range (1,3):
    ax = axes.popitem()
    ax[1].set_ylabel("Response time (seconds)")

plt.show()
plt.clf()

# Head Movement mean for each challenge in VR
print ("\nMean head movement using VR per complexity")
print ('=' * 40)
df = pd.DataFrame()
for i in range (1,4):
    column = vr['Head Movement'].loc[vr['Challenge']==i].reset_index()
    column = column['Head Movement']
    print ("Mean movement for complexity "+ str(i) + ": {0:.4f}".format(column.mean()))
    df ["Complexity "+str(i)] = column
ax = df.boxplot(return_type='axes')
ax.set_title("Mean head movement using VR per complexity")
ax.set_ylabel("Head movement (relative)")
plt.show()
plt.clf()

# Head movement for correct answers vs not correct answers
print ("\nMean head movement using VR per answer type")
print ('=' * 40)
df = pd.DataFrame()

column = vr['Head Movement'].loc[vr['Correct Answer']==0].reset_index()
column = column['Head Movement']
print ("Mean movement for incorrect answers: {0:.4f}".format(column.mean()))
df ["Incorrect"] = column
column = vr['Head Movement'].loc[vr['Correct Answer']==1].reset_index()
column = column['Head Movement']
print ("Mean movement for correct answers: {0:.4f}".format(column.mean()))
df ["Correct"] = column

ax = df.boxplot(return_type='axes')
ax.set_title("Mean head movement using VR per answer type")
ax.set_ylabel("Head movement (relative)")
ax.set_xlabel("Answer type")
plt.show()
plt.clf()