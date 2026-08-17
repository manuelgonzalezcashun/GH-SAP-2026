using UnityEngine;

public class PlayerLoseState : BattleState
{
    public PlayerLoseState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        EventBus.Raise(new DisplayBattleTextEvent { battleText = "Player Lost..." });
        EventBus.Raise(new EndBattleEvent());
        InputHandler.ChangeActionMaps(InputHandler.playerInput);

        _system.UpdateAttackPhaseFlag(false);
        _system.UpdateMovePhaseFlag(false);
        _system.ShowBattleCanvas(false);
    }
}