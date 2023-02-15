using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using States;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<StateMachine> StateMachines = new List<StateMachine>();
    public GameFlowStateMachine GameFlowStateMachine;
    
    private Player _player;
    [SerializeField]
    private GameObject _playerObject;
    
    [SerializeField]
    private GridManager grid;
    
    [SerializeField]
    private Vector2Int startPoint;
    
    [SerializeField]
    private Vector2Int endPoint;
    


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
        GameFlowStateMachine.Init();
        
        StateMachines.Add(GameFlowStateMachine);
        GameFlowStateMachine.StartMachine();
        
        _player = new Player();
        _player.Init(GameFlowStateMachine);
        MovePlayer(startPoint);
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
        if (displacement == endPoint)
        {
            EndGame();
        }
        else
        {
            grid.MoveObjectOnGrid(_playerObject,displacement);
        }
    }

    private void OnDestroy()
    {
        _player?.CleanUp();
    }

    public void EndGame()
    {
        GameFlowStateMachine.EndGame();
    }
}
