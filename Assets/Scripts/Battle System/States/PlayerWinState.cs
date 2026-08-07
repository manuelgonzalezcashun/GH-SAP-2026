using UnityEngine;

public class PlayerWinState : BattleState
{
    public PlayerWinState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        EventBus.Raise(new DisplayBattleTextEvent { battleText = "Player Won!" });
        EventBus.Raise(new EndBattleEvent());
        InputHandler.ChangeActionMaps(InputHandler.playerInput);

        _system.UpdateAttackPhaseFlag(false);
        _system.UpdateMovePhaseFlag(false);
        _system.ShowBattleCanvas(false);
    }
}
