using UnityEngine;

public class PlayerLoseState : BattleState
{
    public PlayerLoseState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        // Player has 0 health
        Debug.Log($"{_system.Player.Name} Lost...");
        BattleEvents.EndBattle();
    }
}