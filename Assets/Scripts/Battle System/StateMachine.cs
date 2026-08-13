using System;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State _currentState;
    [SerializeField] private float delay;
    public float Delay => delay;
    public virtual void SetState(State state)
    {
        if (_currentState != null)
            _currentState.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
