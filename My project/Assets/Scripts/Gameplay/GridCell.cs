using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private Material walkableMat;

        [SerializeField] private Material nonWalkableMat;
        
        [SerializeField] public Material startPointMat;
        [SerializeField] public Material endPointMat;

        [SerializeField]
        private MeshRenderer currentMat ;

        [SerializeField]
        private bool _walkable;

        public bool Walkable
        {
            get { return _walkable; }
            set
            {
                if (value)
                {
                    currentMat.material = walkableMat;
                }
                else
                {
                    currentMat.material = nonWalkableMat;
                }

                _walkable = value;
            }
        }

        public void SetStartPoint()
        {
            currentMat.material = startPointMat;
        }

        public void SetEndpoint()
        {
            currentMat.material = endPointMat;
        }



        private void Awake()
        {

        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(GridCell))]
    public class GridSwitcher : Editor
    {
        private void OnSceneGUI()
        {
            Event e = Event.current;

            if (e.type == EventType.KeyUp)
            {
                if (KeyCode.RightControl == e.keyCode)
                {
                    GridCell current = Selection.activeObject.GetComponent<GridCell>();
                    if (current != null)
                    {
                        current.Walkable = !current.Walkable;
                    }
                }
            }
        }
    }
#endif
}
