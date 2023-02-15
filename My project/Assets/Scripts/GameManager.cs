using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using StateMachine = States.StateMachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<StateMachine> StateMachines = new List<StateMachine>();
    public GameFlowStateMachine GameFlowStateMachine;
    
    private Player _player;
    [SerializeField]
    private GameObject _playerObject;
    
    
    private GridManager _currentGrid;
    private int _currentLevel;
    
   

    [SerializeField] private GameObject MainMenu;
    
    [SerializeField] private GameObject EndMenu;

    [SerializeField]
    private List<GridManager> Levels;




    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameFlowStateMachine = new GameFlowStateMachine();
        GameFlowStateMachine.MainMenu = MainMenu;
        GameFlowStateMachine.EndMenu = EndMenu;
        GameFlowStateMachine.Init();
        
        StateMachines.Add(GameFlowStateMachine);
        GameFlowStateMachine.StartMachine();
        
        _player = new Player();
        _player.Init(GameFlowStateMachine);
        _currentLevel = 0;
        SetLevel(_currentLevel);
    }

 

    // Update is called once per frame
    void Update()
    {
        foreach (var stateMachine in StateMachines)
        {
            stateMachine.Update();
        }
    }


    public void MovePlayer(Vector2Int displacement)
    {
        Vector2Int newGridPos = _currentGrid.MoveObjectOnGrid(_playerObject,displacement);
        
        if (_currentGrid.AtEndPoint(_playerObject))
        {
            _currentLevel++;
            SetLevel(_currentLevel);
        }
    }

    private void OnDestroy()
    {
        _player?.CleanUp();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void EndGame()
    {
        GameFlowStateMachine.EndGame();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SetLevel(int i)
    {
        if (i >= Levels.Count)
        {
            EndGame();
            return;
        }
        if (i > 0)
        {
            _currentGrid.gameObject.SetActive(false);
        }
        
        _currentGrid = Levels[i];
        _currentGrid.gameObject.SetActive(true);
        _currentGrid.SetAtStart(_playerObject);
        _currentGrid.SetMapMarkers();
    }
}
