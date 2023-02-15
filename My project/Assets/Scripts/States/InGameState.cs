using System;
using UnityEngine;

namespace States
{
    public class InGameState:BaseState
    {
        private GameFlowStateMachine _sm;
        public Action InputReceived;
        
        public InGameState(StateMachine stateMachine) : base(nameof(InGameState), stateMachine)
        {
            _sm = (GameFlowStateMachine)stateMachine;
        }

        public override void EnterState()
        {
            Debug.Log("Game is Now Playing");
        }

        public override void ExitState()
        {
            Debug.Log("Game is not Playing");
        }

        public override void UpdateState()
        {
            Debug.Log("Game is ongoing");
            if (Input.GetKeyUp(KeyCode.Q))
            {
                _sm.ChangeState(_sm.EndGameState);
            }

            
            if (Input.anyKeyDown)
            {
                InputReceived?.Invoke();
            }
                
        }
    }
}