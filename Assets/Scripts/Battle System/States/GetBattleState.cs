public class GetBattleState : BattleState
{
    public GetBattleState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        if (_system.AttackPhaseComplete && _system.MovePhaseComplete)
        {
            _system.UpdateAttackPhaseFlag(false);
            _system.UpdateMovePhaseFlag(false);
            _system.SetState(new InitiativeCheckState(_system));
        }

    }
}