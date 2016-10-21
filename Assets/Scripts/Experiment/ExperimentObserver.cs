using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Experiment
{
    public interface ExperimentObserver
    {
        void onStageChange(float time, float headMovement);
        void onChallengeChange(float time, float headMovement);
        void onFinish(float time, float headMovement);
        void onStart();
        void onReset();
        void onUpdate();
        void onAnswer();
        void onSubscribe();
        void onUnsubscribe();
    }
}
