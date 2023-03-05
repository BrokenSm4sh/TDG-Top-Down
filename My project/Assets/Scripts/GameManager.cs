using System;
using System.Collections.Generic;
using Gameplay;
using States;
using UnityEngine;
using StateMachine = States.StateMachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<StateMachine> StateMachines = new List<StateMachine>();
    public GameFlowStateMachine GameFlowStateMachine;
    
    private Player _player;
    [SerializeField]
    private GameObject _playerObject;
    
    
    //private GridManager _currentGrid;
    private Level _currentGridLevel;

    public Level CurrentGridLevel => _currentGridLevel;

    private int _currentLevel;

    public bool hasKey;




    [SerializeField] private GameObject MainMenu;
    
    [SerializeField] private GameObject EndMenu;

    [SerializeField]
    private List<Level> Levels;

    public event Action GameFlowInitiated;

    public GameObject PlayerObject
    {
        get
        {
            return _playerObject;
        }
    }

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

        if (GameFlowInitiated != null)
        {
            GameFlowInitiated();
        }


        StateMachines.Add(GameFlowStateMachine);
        GameFlowStateMachine.StartMachine();
        
        _player = new Player();
        _player.Init(GameFlowStateMachine);
        _currentLevel = 0;
        SetLevel(_currentLevel);
        hasKey = false;
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
        Vector2Int newGridPos = _currentGridLevel.MoveObjectOnGrid(_playerObject,displacement);
        
        if (_currentGridLevel.CurrentGrid.AtEndPoint(_playerObject))
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
            _currentGridLevel.gameObject.SetActive(false);
        }
        
        _currentGridLevel = Levels[i];
        _currentGridLevel.gameObject.SetActive(true);
        _currentGridLevel.CurrentGrid.SetAtStart(_playerObject);
        _currentGridLevel.CurrentGrid.SetMapMarkers();
    }

    public void ChangeCurrentLevelLayout()
    {
        Levels[_currentLevel].SwitchLayout();
    }
}
