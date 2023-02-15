
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public abstract class BaseState
    {
        public string name;
        
        protected StateMachine stateMachine;

        public BaseState(string name, StateMachine stateMachine)
        {
            this.name = name;
            this.stateMachine = stateMachine;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();

    }
}
