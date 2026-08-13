public class PlayerEscapeState : BattleState
{
    public PlayerEscapeState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        EventBus.Raise(new EndBattleEvent());
        InputHandler.ChangeActionMaps(InputHandler.playerInput);

        _system.UpdateAttackPhaseFlag(false);
        _system.UpdateMovePhaseFlag(false);
        _system.ShowBattleCanvas(false);
    }
}
