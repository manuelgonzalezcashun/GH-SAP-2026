using UnityEngine;

public class PlayerLoseState : BattleState
{
    public PlayerLoseState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        Debug.Log("Player Lost...");
        EventBus.Raise(new EndBattleEvent());
        InputHandler.ChangeActionMaps(InputHandler.playerInput);
        _system.ShowBattleCanvas(false);
    }
}