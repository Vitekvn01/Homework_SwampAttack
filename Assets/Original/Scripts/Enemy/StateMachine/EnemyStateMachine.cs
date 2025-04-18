using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    
    private Player _target;
    private State _currentState;

    public State CurrentState => _currentState;

    private void Start()
    {
        _target = GetComponent<Enemy>().GetTarget();
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }

        var nextState = _currentState.GetNextState();

        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            
        }

        _currentState = nextState; 

        if (_currentState != null) 
        {
            _currentState.Enter(_target);
        }
    }

    private void Reset(State StartState)
    {
        _currentState = StartState;
        if (_currentState != null)
        {
            _currentState.Enter(_target);
        }
    }
}
