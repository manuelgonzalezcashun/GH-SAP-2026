using System;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected BattleState _currentState;
    [SerializeField] private float delay;
    public float Delay => delay;
    public void SetState(BattleState state)
    {
        _currentState = state;
        _currentState.EnterState();
    }
}
