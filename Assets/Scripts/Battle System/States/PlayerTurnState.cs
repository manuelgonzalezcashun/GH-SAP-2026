using System.Collections;
using UnityEngine;

public class PlayerTurnState : BattleState
{
    public PlayerTurnState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        BattleEvents.ShowBattleOptions(true);
    }

    public override IEnumerator Attack()
    {
        BattleEvents.ShowBattleOptions(false);

        var target = _system.Opponent;
        var attacker = _system.Player;

        bool isFainted = target.TakeDamage(attacker.Power);
        yield return new WaitForSeconds(_system.Delay);

        BattleState state = isFainted
        ? new CheckOpponentTeamState(_system)
        : new OpponentTurnState(_system);

        _system.SetState(state);
    }
    public override IEnumerator Heal()
    {
        BattleEvents.ShowBattleOptions(false);

        var healer = _system.Player;
        healer.Heal(healer.Healing);
        yield return new WaitForSeconds(_system.Delay);

        BattleState state = new OpponentTurnState(_system);
        _system.SetState(state);
    }
}
