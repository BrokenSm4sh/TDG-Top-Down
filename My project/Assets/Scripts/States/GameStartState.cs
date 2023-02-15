using UnityEngine;

namespace States
{
    public class GameStartState: BaseState
    {
        private GameFlowStateMachine _sm;
        private GameObject _mainMenu;

    
        public GameStartState(StateMachine stateMachine, GameObject gameObject) : base(nameof(GameStartState), stateMachine)
        {
            _sm = stateMachine as GameFlowStateMachine;
            _mainMenu = gameObject;
        }

        public override void EnterState()
        {
            Debug.Log("Welcome to game push p to start");
            _mainMenu.SetActive(true);
        }

        public override void ExitState()
        {
            Debug.Log("Enjoy Game");
            _mainMenu.SetActive(false);
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