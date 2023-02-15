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
        private Vector2Int startPoint;
    
        [SerializeField]
        private Vector2Int endPoint;
        
        
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


        public Vector2Int MoveObjectOnGrid(GameObject obj, Vector2Int displacement)
        {
            Vector3Int currentPos = grid.WorldToCell(obj.transform.position);
            Vector3Int newposition = currentPos + new Vector3Int(displacement.x, 0, displacement.y);
            if (MoveObjectToGridPosition(obj, newposition))
            {
                return ToGridCoord(newposition);
            }
            return ToGridCoord(currentPos);
        }

        public bool MoveObjectToGridPosition(GameObject obj, Vector3Int newPosition)
        {
            if (Walkable(ToGridCoord(newPosition)))
            {
                obj.transform.position = grid.GetCellCenterLocal(newPosition);
                Debug.Log("moved to" + newPosition);
                return true;
            }

            return false;
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

        public void SetMapMarkers()
        {
            GridCell cell;
            
            if (_fullGrid.ContainsKey(startPoint))
            { 
                cell = _fullGrid[startPoint]; 
                cell.SetStartPoint();
            }
            if (_fullGrid.ContainsKey(endPoint))
            {
                cell = _fullGrid[endPoint];
                cell.SetEndpoint();
            }
        }

        public void SetAtStart(GameObject objectToStart)
        {
            MoveObjectToGridPosition(objectToStart,new Vector3Int(startPoint.x,0,startPoint.y));
        }

        public bool AtEndPoint(GameObject obj)
        {
           return grid.LocalToCell(obj.transform.position) == new Vector3Int(endPoint.x,0,endPoint.y);
        }
    }
}