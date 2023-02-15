using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

using Vector2 = System.Numerics.Vector2;

namespace Gameplay
{
    public class GridManager: MonoBehaviour
    { 
        
        [SerializeField]
        private Grid grid;
        
        [SerializeField]
        private GridCell nonWalkSurface;

        [SerializeField]
        private Vector2Int gridSize;
        
        
        [SerializeField]
        // private List<GridCell> _fullGrid = new List<GridCell>();
        private Dictionary<Vector2Int,GridCell> _fullGrid = new Dictionary<Vector2Int, GridCell>();
        
        
        [ContextMenu("Generate")]
        public void Generate()
        {
            
            for (int i = transform.childCount-1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    //_fullGrid.Add(Instantiate(nonWalkSurface, grid.GetCellCenterLocal(new Vector3Int(x, 0, y)), Quaternion.identity,transform));
                    Instantiate(nonWalkSurface, grid.GetCellCenterLocal(new Vector3Int(x, 0, y)), Quaternion.identity,transform);
                    //cellnumWrapper.Add(new Vector2Int(x,y));
                    //cellWrapper.Add(Instantiate(nonWalkSurface, grid.GetCellCenterLocal(new Vector3Int(x, 0, y)), Quaternion.identity,transform));
                }
            }
        }

        private void Awake()
        {
            foreach (Transform tf in transform)
            {
                _fullGrid.Add(ToGridCoord(grid.LocalToCell(tf.position)),tf.GetComponent<GridCell>());
            }
        }

        private Vector2Int ToGridCoord(Vector3Int vect)
        {
            return new Vector2Int(vect.x, vect.z);
        }


        public void MoveObjectOnGrid(GameObject obj, Vector2Int displacement)
        {
            Vector3Int currentPos = grid.WorldToCell(obj.transform.position);
            Vector3Int newposition = currentPos + new Vector3Int(displacement.x, 0, displacement.y);
            if (Walkable(ToGridCoord(newposition)))
            {
                obj.transform.position = grid.GetCellCenterLocal(newposition);
                Debug.Log("moved to" + newposition);
            }
        }

        private bool Walkable(Vector2Int newPosition)
        {
            if (_fullGrid.ContainsKey(newPosition))
            {
                GridCell cell = _fullGrid[newPosition];
                return cell.Walkable;
            }
            else
            {
                return false;
            }
        }
    }
}