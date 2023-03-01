using System;
using UnityEngine;

namespace Gameplay
{
    public class LevelLayoutSwitcher:MonoBehaviour
    {
        private void Awake()
        {
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
            GameManager.Instance.ChangeCurrentLevelLayout();
        }

        private void OnDisable()
        {
            GameManager.Instance.GameFlowStateMachine.InGameState.InputReceived -= ManageKey;
        }

        private void OnEnable()
        {
            GameManager.Instance.GameFlowStateMachine.InGameState.InputReceived += ManageKey;
        }
    }
}