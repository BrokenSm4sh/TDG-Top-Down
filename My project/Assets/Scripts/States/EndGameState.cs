using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    public class EndGameState:BaseState
    {
        private GameFlowStateMachine _sm;

        private GameObject _endScreen;

        public EndGameState(StateMachine stateMachine, GameObject EndScreen) : base(nameof(EndGameState), stateMachine)
        {
            _endScreen = EndScreen;
        }

        public override void EnterState()
        {
            Debug.Log("Game is Endin");
            _endScreen.SetActive(true);
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
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}