using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.GameProgrammingPatterns.State
{
    public abstract class State
    {
        protected StateMachine machine;

        public virtual void Enter(StateMachine machine)
        {
            this.machine = machine;
        }

        public abstract void Exit();

        public abstract void StateBehaviour();

        public abstract State StateTransition();

    }
}