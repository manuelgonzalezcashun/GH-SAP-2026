using System.Collections;
using System.Collections.Generic;

public class SetupBattleState : BattleState
{
    public SetupBattleState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        // TODO: Check which monster goes first (Higher Initiative Goes First)

        var playerParty = _system.PlayerParty.Battlers;
        var oppParty = _system.OpponentParty.Battlers;

        SetPartyToZone(playerParty, Zone.P_BACKLINE);
        SetPartyToZone(oppParty, Zone.O_BACKLINE);

        // _system.SetState(new SpeedCheckState(_system));
    }

    public void SetPartyToZone(IEnumerable<Battler> party, Zone zone)
    {
        foreach (var battler in party) BattleEvents.SetBattlerInZone(battler, zone);
    }
}
