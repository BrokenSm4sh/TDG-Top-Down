using UnityEngine;

namespace States
{
    public class EndGameState:BaseState
    {
        private GameFlowStateMachine _sm;

        
        public EndGameState(StateMachine stateMachine) : base(nameof(EndGameState), stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Game is Endin");
        }

        public override void ExitState()
        {
            Application.Quit();
        }

        public override void UpdateState()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                ExitState();
            }
        }
    }
}