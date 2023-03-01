using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private GridManager firstLayout;
    
    [SerializeField]
    private GridManager secondLayout;
    
    [SerializeField]
    private LevelLayoutSwitcher layoutSwitch;
        
    [SerializeField]
    private Vector2Int layoutSwitchPosition;

    public GridManager CurrentGrid
    {
        get;
        private set;
    }

    private void Awake()
    {
        firstLayout.gameObject.SetActive(true);
        secondLayout.gameObject.SetActive(false);
        CurrentGrid = firstLayout;
        
        LevelLayoutSwitcher lSwitch = Instantiate(layoutSwitch,transform);
        MoveObjectOnGrid(lSwitch.gameObject, layoutSwitchPosition);
    }

    public void SwitchLayout()
    {
        if (firstLayout.gameObject.activeSelf)
        {
            firstLayout.gameObject.SetActive(false);
            secondLayout.gameObject.SetActive(true);
            CurrentGrid = secondLayout;
        }
        else
        {
            firstLayout.gameObject.SetActive(true);
            secondLayout.gameObject.SetActive(false);
            CurrentGrid = firstLayout;
        }
    }

    public Vector2Int MoveObjectOnGrid(GameObject movObject, Vector2Int displacement)
    {
        return CurrentGrid.MoveObjectOnGrid(movObject, displacement);
    }
}
