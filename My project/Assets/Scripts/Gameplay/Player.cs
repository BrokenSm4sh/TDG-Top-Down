using System.Runtime.CompilerServices;
using States;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Player
    {
        private int _hp;

        public void Init(GameFlowStateMachine sm, int baseHp = 3)
        {
            GameManager.Instance.GameFlowStateMachine.InGameState.InputReceived += ManageKey;
            _hp = baseHp;
        }

        private void ManageKey()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(new Vector2Int(-1,0));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(new Vector2Int(1,0));

            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Move(new Vector2Int(0,-1));

            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                Move(new Vector2Int(0,1));

            }
        }

        public void Move(Vector2Int displacement)
        {
            GameManager.Instance.MovePlayer(displacement);
        }

        public int TakeDamage(int damage = 1)
        {
            _hp -= damage;
            return _hp;
        }

        public void CleanUp()
        {
            GameManager.Instance.GameFlowStateMachine.InGameState.InputReceived -= ManageKey;   
        }
    }
}