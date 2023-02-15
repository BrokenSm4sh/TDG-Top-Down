using System.Runtime.CompilerServices;
using UnityEngine;

namespace States
{
    public class GameFlowStateMachine : StateMachine
    {
        public GameStartState GameStartState;
        
        public InGameState InGameState;

        public EndGameState EndGameState;

        public GameObject MainMenu;
        public GameObject PauseMenu;
        public GameObject EndMenu { get; set; }

        public void Init()
        {
            GameStartState = new GameStartState(this,MainMenu);
            InGameState = new InGameState(this);
            EndGameState = new EndGameState(this,EndMenu);
            _currentState = GameStartState;
        }

        public void StartMachine()
        {
            _currentState.EnterState();
        }

        public void EndGame()
        {
            ChangeState(EndGameState);
        }
        
        public void NextLevel()
        {
            ChangeState(EndGameState);
        }


    }
}