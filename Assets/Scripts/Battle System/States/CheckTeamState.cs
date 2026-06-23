public class CheckPlayerTeamState : BattleState
{
    public CheckPlayerTeamState(BattleSystem system) : base(system) { }
    public override void EnterState()
    {
        var battler = _system.PlayerParty.GetBattler();

        BattleState state = battler == null
        ? new PlayerLoseState(_system)
        : new SetupBattleState(_system);

        _system.SetState(state);
    }
}

public class CheckOpponentTeamState : BattleState
{
    public CheckOpponentTeamState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        var battler = _system.OpponentParty.GetBattler();
        BattleState state = battler == null
        ? new PlayerWinState(_system)
        : new SetupBattleState(_system);

        _system.SetState(state);
    }
}