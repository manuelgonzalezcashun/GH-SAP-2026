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
        _system.SetState(new PlayerAttackState(_system));
        yield return null;
    }
    public override IEnumerator Heal()
    {
        BattleEvents.ShowBattleOptions(false);

        var healer = _system.ActiveBattler;

        // BattleEvents.SetBattleText($"{healer.Name} is recovering strength!");
        Debug.Log($"{healer.Name} is recovering strength!");

        healer.Heal(healer.Healing);
        yield return new WaitForSeconds(_system.Delay);

        BattleState state = new AttackSetupState(_system);
        _system.SetState(state);
    }
}
