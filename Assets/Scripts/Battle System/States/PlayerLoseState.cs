using UnityEngine;

public class PlayerLoseState : BattleState
{
    public PlayerLoseState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        Debug.Log("Player Lost...");
    }
}