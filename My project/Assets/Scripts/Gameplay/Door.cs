using UnityEngine;

namespace Gameplay
{
    public class Door:MonoBehaviour
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
                Open();
            }
        }

        private void Open()
        {
            if (GameManager.Instance.CurrentGridLevel.CurrentGrid.IsObjectInRange(gameObject, GameManager.Instance.PlayerObject, 1)
                && GameManager.Instance.hasKey)
            {
                gameObject.SetActive(false);
            }
        }
    }
}