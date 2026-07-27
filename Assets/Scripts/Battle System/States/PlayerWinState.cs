using UnityEngine;

public class PlayerWinState : BattleState
{
    public PlayerWinState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        Debug.Log("Player Won!");
        EventBus.Raise(new EndBattleEvent());
        InputHandler.ChangeActionMaps(InputHandler.playerInput);
        _system.ShowBattleCanvas(false);
    }
}
