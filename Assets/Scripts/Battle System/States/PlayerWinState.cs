using UnityEngine;

public class PlayerWinState : BattleState
{
    public PlayerWinState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        // Player Has More Health than Enemy
        Debug.Log($"{_system.Player.Name} Win!");
        BattleEvents.EndBattle();
    }
}
