using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class OpponentAttackState : BattleState
{
    public OpponentAttackState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        _system.StartCoroutine(Attack());
    }

    public override IEnumerator Attack()
    {
        var attacker = _system.ActiveBattler;

        List<Battler> eligibleTargets = _system.AllBattlers.Where(battler => battler.Team == Team.PLAYER).ToList();
        int index = Random.Range(0, eligibleTargets.Count);
        var target = eligibleTargets[index];

        Debug.Log($"{attacker.Name} Attacked {target.Name}!");
        yield return new WaitForSeconds(_system.Delay);

        target.TakeDamage(attacker.Power);
        _system.SetState(new AttackSetupState(_system));
    }
}