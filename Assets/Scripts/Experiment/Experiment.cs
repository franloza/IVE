using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Experiment;

namespace Assets.Scripts.Experiment
{
    public class Experiment : ExperimentObservable
    {
        //ANSWER ENUM
        public enum ExperimentAnswer {POSITIVE,NEGATIVE,NO_ANSWER}

        //CONSTANTS
        //Maximum time for each level (seconds)
        private readonly float[] MAX_TIME  = { 10f, 20f, 30f };

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

        public long Id
        {
            get
            {
                return _id;
            }

            private set
            {
                _id = value;
            }
        }

        public bool Solution
        {
            get
            {
                return _solution;
            }

            set
            {
                _solution = value;
            }
}

        public int NumCorrect
        {
            get
            {
                return _numCorrect;
            }

            private set
            {
                _numCorrect = value;
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
            this.Stage = 2;
            this.Challenge = 1;
            this.TimeLeft = MaxTime;
            this.Finished = false;
            this.Paused = true;

            Tuple<long, int> tuple = ExperimentLogger.loadId();
            this.Id = tuple.First;
            this.NumCorrect = tuple.Second;
             

            _answer = ExperimentAnswer.NO_ANSWER;
            foreach (ExperimentObserver obs in this._observers) { obs.onReset(); }
        }

        public void answer(bool answer)
        {
            //if (!Paused && !Finished)
            //{
                if (answer) _answer = ExperimentAnswer.POSITIVE;
                else _answer = ExperimentAnswer.NEGATIVE;
                nextChallenge();
            //}
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
                foreach (ExperimentObserver obs in this._observers) { obs.onFinish(time, headMovement); }
            }
            else
            {
                foreach (ExperimentObserver obs in this._observers) { obs.onStageChange(time, headMovement); }
            }
            this.Paused = true;
        }

        private void nextChallenge()
        {
            //Get metrics
            float time = this.MaxTime - this.TimeLeft;
            //TODO: Get head movement
            float headMovement = 0f;
            bool correctAnswer;
            if (_answer == ExperimentAnswer.NO_ANSWER) correctAnswer = false; //No answer = no correct answer
            else if (_answer == ExperimentAnswer.POSITIVE) //Only correct if the solution is true
                correctAnswer = _solution;
            else correctAnswer = !_solution; //Only correct if the solution is false

            this.NumCorrect += correctAnswer ? 1 : 0;

            //Log results
            ExperimentLogger.Log(this.Id,this.Stage,this.Challenge,time,headMovement,correctAnswer);

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
                foreach (ExperimentObserver obs in this._observers) { obs.onChallengeChange(time,headMovement);}
            }     
        }

        //ATTRIBUTES
        private long _id;
        //Stage number (1..3)
        private int _stage;
        //Challenge number (1..3)
        private int _challenge;
        //Number of correct answers
        private int _numCorrect;
        //Countdown timer 
        private float _timeLeft;
        //Experiment is paused
        private bool _paused;
        //Experiment is finished
        private bool _finished;
        //Current solution
        private bool _solution;
        //Current answer
        private ExperimentAnswer _answer;
        //List of observers
        private List<ExperimentObserver> _observers = new List<ExperimentObserver>();

    }
}
