using System.Collections;
using UnityEngine;

public class OpponentTurnState : BattleState
{
    public OpponentTurnState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        var target = _system.Player;
        var attacker = _system.Opponent;

        // Enemy Attack
        BattleEvents.ShowBattleOptions(false);
        bool isFainted = target.TakeDamage(attacker.Power);

        BattleState state = isFainted
        ? new CheckPlayerTeamState(_system)
        : new PlayerTurnState(_system);

        _system.SetState(state);
    }
}
