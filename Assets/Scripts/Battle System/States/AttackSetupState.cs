public class AttackSetupState : BattleState
{
    public AttackSetupState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        if (_system.TurnQueue.Count <= 0)
        {
            _system.UpdateAttackPhaseFlag(true);
            _system.SetState(new GetBattleState(_system));
            return;
        }

        var activeBattler = _system.TurnQueue.Dequeue();
        if (activeBattler.Health <= 0)
        {
            _system.SetState(new AttackSetupState(_system));
            return;
        }

        _system.SetActiveBattler(activeBattler);

        BattleState state = _system.ActiveBattler.Team == Team.PLAYER
        ? new PlayerTurnState(_system)
        : new OpponentAttackState(_system);


        _system.SetState(state);
    }
}