using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Experiment
{
    public class Experiment : ExperimentObservable
    {
        //ANSWER ENUM
        public enum ExperimentAnswer {POSITIVE,NEGATIVE,NO_ANSWER}

        //CONSTANTS
        //Maximum time for each level (seconds)
        private readonly float[] MAX_TIME  = { 5f, 7f, 10f };

        //CONSTRUCTOR
        public Experiment()
        {
            reset();
        }

        //PROPERTIES
        public int Stage
        {
            get
            {
                return _stage;
            }

            private set
            {
                _stage = value;
            }
        }

        public int Challenge
        {
            get
            {
                return _challenge;
            }

            private set
            {
                _challenge = value;
            }
        }

        public float TimeLeft
        {
            get
            {
                return _timeLeft;
            }

            private set
            {
                _timeLeft = value;
            }
        }

        public bool Paused
        {
            get
            {
                return _paused;
            }

            private set
            {
                _paused = value;
            }
        }

        public bool Finished
        {
            get
            {
                return _finished;
            }

            private set
            {
                _finished = value;
            }
        }

        public bool VisualReduction
        {
            get
            {
                return (_stage == 3);
            }
        }

        public float MaxTime
        {
            get
            {
                return (MAX_TIME[(_challenge-1) % MAX_TIME.Length]);
            }
        }

        //PUBLIC METHODS
        public void update()
        {
            if (!this.Paused)
            {
                this.TimeLeft -= Time.deltaTime;
                //Go to the next challange if the time is over
                if (this.TimeLeft < 0.001)
                {
                    this.TimeLeft = 0;
                    nextChallenge();
                }
            }
            foreach (ExperimentObserver obs in this._observers) { obs.onUpdate(); }
        }

        public void start()
        {
            this.TimeLeft = MaxTime;
            this.Finished = false;
            this.Paused = false;
            _answer = ExperimentAnswer.NO_ANSWER;
            foreach (ExperimentObserver obs in this._observers) { obs.onStart(); }
        }

        public void reset()
        {
            this.Stage = 1;
            this.Challenge = 1;
            this.TimeLeft = MaxTime;
            this.Finished = false;
            this.Paused = true;
            _answer = ExperimentAnswer.NO_ANSWER;
            foreach (ExperimentObserver obs in this._observers) { obs.onReset(); }
        }

        public void answer(bool answer)
        {
            if (answer) _answer = ExperimentAnswer.POSITIVE;
            else _answer = ExperimentAnswer.NEGATIVE;
            nextChallenge();
        }

        public void subscribe(ExperimentObserver obs)
        {
            this._observers.Add(obs);
            obs.onSubscribe();
        }

        public void unsubscribe(ExperimentObserver obs)
        {
            this._observers.Remove(obs);
            obs.onUnsubscribe();
        }

        //PRIVATE METHODS
        private void nextStage(float time, float headMovement, bool correctAnswer)
        {
            this.Stage++;
            if (this.Stage > 3)
            {
                this.Finished = true;
                foreach (ExperimentObserver obs in this._observers) { obs.onFinish(time, headMovement, correctAnswer); }
            }
            else
            {
                foreach (ExperimentObserver obs in this._observers) { obs.onStageChange(time, headMovement, correctAnswer); }
            }
            this.Paused = true;
        }

        private void nextChallenge()
        {
            //Get metrics
            float time = this.MaxTime - this.TimeLeft;
            //TODO: Get head movement and correct anwser
            float headMovement = 0f;
            bool correctAnswer = (_answer == ExperimentAnswer.NO_ANSWER)?false:true; //Check if the given answer is correct

            this.Paused = true;
            this.Challenge++;
            
            if (this.Challenge > 3)
            {
                this.Challenge = 1;
                nextStage(time,headMovement,correctAnswer);
            }
            else
            {
                this.TimeLeft = MaxTime;
                this.Paused = false;
                _answer = ExperimentAnswer.NO_ANSWER;
                foreach (ExperimentObserver obs in this._observers) { obs.onChallengeChange(time,headMovement,correctAnswer); }
            }     
        }

        //ATTRIBUTES
        //Stage number (1..3)
        private int _stage;
        //Challenge number (1..3)
        private int _challenge;
        //Countdown timer 
        private float _timeLeft;
        //Experiment is paused
        private bool _paused;
        //Experiment is finished
        private bool _finished;
        //Current answer
        private ExperimentAnswer _answer;
        //List of observers
        private List<ExperimentObserver> _observers = new List<ExperimentObserver>();

    }
}
