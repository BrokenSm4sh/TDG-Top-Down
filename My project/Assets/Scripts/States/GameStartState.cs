using UnityEngine;

namespace States
{
    public class GameStartState: BaseState
    {
        private GameFlowStateMachine _sm;

        public GameStartState(StateMachine stateMachine) : base(nameof(GameStartState), stateMachine)
        {
            _sm = stateMachine as GameFlowStateMachine;
        }

        public override void EnterState()
        {
            Debug.Log("Welcome to game push p to start");
        }

        public override void ExitState()
        {
            Debug.Log("Enjoy Game");
        }

        public override void UpdateState()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                _sm.ChangeState(_sm.InGameState);
            }
        }
    }
}