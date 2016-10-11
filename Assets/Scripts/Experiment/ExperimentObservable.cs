using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Experiment
{
    interface ExperimentObservable
    {
        void subscribe(ExperimentObserver obs);
        void unsubscribe(ExperimentObserver obs);
    }
}
