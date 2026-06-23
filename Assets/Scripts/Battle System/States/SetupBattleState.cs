using UnityEngine;

public class SetupBattleState : BattleState
{
    public SetupBattleState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        // TODO: Check which monster goes first (Higher Initiative Goes First)

        var playerBattler = _system.PlayerParty.GetBattler();
        var oppBattler = _system.OpponentParty.GetBattler();

        _system.SetupBattle(playerBattler, oppBattler);
        _system.SetState(new PlayerTurnState(_system));
    }
}
