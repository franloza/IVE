using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Experiment
{
    public interface ExperimentObserver
    {
        void onStageChange(float time, float headMovement, bool correctAnswer);
        void onChallengeChange(float time, float headMovement, bool correctAnswer);
        void onFinish(float time, float headMovement, bool correctAnswer);
        void onStart();
        void onReset();
        void onUpdate();
        void onAnswer();
        void onSubscribe();
        void onUnsubscribe();
    }
}
