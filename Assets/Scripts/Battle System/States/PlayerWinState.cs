using UnityEngine;

public class PlayerWinState : BattleState
{
    public PlayerWinState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        Debug.Log("Player Won!");

    }
}
