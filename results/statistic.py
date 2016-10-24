import pandas
# Read in the airports data.
results = pandas.read_csv("results2.csv", header=0)
novr = results.loc[results['Stage'] == 1]
vr = results.loc[results['Stage'] == 2] 
vr = vr.drop('Stage',axis=1) 
novr = novr.drop('Stage',axis=1) 

# Error rate Non-VR vs VR
#   No VR
print ("Error rate Non-VR vs VR")
print ('=' * 20)
percentage = novr['Correct Answer'].loc[novr['Correct Answer'] == 1].count() / novr['Correct Answer'].count() * 100
error = novr['Correct Answer'].loc[novr['Correct Answer'] == 0].count() / novr['Correct Answer'].count()
print ("Percentage of correct anwsers without VR: {0:.2f}%".format(percentage))
print ("Error rate without VR: {0:.2f}\n".format(error))
#   VR
percentage = vr['Correct Answer'].loc[vr['Correct Answer'] == 1].count() / vr['Correct Answer'].count() * 100
error = vr['Correct Answer'].loc[vr['Correct Answer'] == 0].count() / vr['Correct Answer'].count()
print ("Percentage of correct anwsers with VR: {0:.2f}%".format(percentage))
print ("Error rate with VR: {0:.2f}".format(error))

#Plot error rate evolution according to complexity
# Mean time for answering Non-VR vs VR per stage and total
# Head Movement mean for each stage in VR
# Head movement for correct answers vs not correct answers
