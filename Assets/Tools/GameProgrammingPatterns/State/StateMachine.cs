using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.GameProgrammingPatterns.State
{
    public abstract class StateMachine
    {
        public State CurrentState { get; private set; }

        public event Action<State> OnChangeToState;

        StateMachine(State state, Action<State> action)
        {
            CurrentState = state;
            CurrentState.Enter(this);

            OnChangeToState = action;
        }

        protected void TransitionTo(State state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter(this);

            OnChangeToState?.Invoke(state);
        }




    }
}