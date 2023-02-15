using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace States
{
    public class StateMachine
    {
        protected BaseState _currentState;

        public void Enter()
        {
            if (_currentState != null)
            {
                _currentState.EnterState();
            }
        }

        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.UpdateState();
            }
        }
        
        public void Exit()
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
        }
        
        public void ChangeState(BaseState newState)
        {
            _currentState.ExitState();

            _currentState = newState;
            _currentState.EnterState();
        }
        
        
    }
}