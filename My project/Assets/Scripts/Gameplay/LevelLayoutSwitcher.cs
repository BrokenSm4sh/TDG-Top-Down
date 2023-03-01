using System;
using UnityEngine;

namespace Gameplay
{
    public class LevelLayoutSwitcher:MonoBehaviour
    {
        private bool isUsable;
        
        private void Start()
        {
            GameManager.Instance.GameFlowInitiated += OnGameFlowInitiated;
            
            isUsable = false;
        }

        
        private void OnGameFlowInitiated()
        {
            GameManager.Instance.GameFlowInitiated -= OnGameFlowInitiated;
            GameManager.Instance.GameFlowStateMachine.InGameState.InputReceived += ManageKey;
        }

        private void ManageKey()
        {
            if (Input.GetKey(KeyCode.F))
            {
                SwitchLayout();
            }
        }

        private void SwitchLayout()
        {
            if (GameManager.Instance.CurrentGridLevel.CurrentGrid.IsObjectInRange(gameObject, GameManager.Instance.PlayerObject, 1))
            {
                GameManager.Instance.ChangeCurrentLevelLayout();
            }
        }

        private void OnEnable()
        {
            if (isUsable)
            {
                GameManager.Instance.GameFlowStateMachine.InGameState.InputReceived += ManageKey;
            }
        }

        private void OnDisable()
        {
            if (isUsable)
            {
                GameManager.Instance.GameFlowStateMachine.InGameState.InputReceived -= ManageKey;
            }

        }
    }
}