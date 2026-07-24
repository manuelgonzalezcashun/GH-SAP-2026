using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class OpponentAttackState : BattleState
{
    public OpponentAttackState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        Move move = null; // * Band-Aid Solution
        _system.StartCoroutine(Attack(move));
    }

    public override IEnumerator Attack(Move move)
    {
        var attacker = _system.ActiveBattler;
        Move selectedMove = attacker.Moves[Random.Range(0, attacker.Moves.Length)];


        List<Battler> eligibleTargets = _system.AllBattlers.Where(battler => battler.Team == Team.PLAYER && battler.Health > 0).ToList();
        if (eligibleTargets.Count <= 0)
        {
            _system.SetState(new PlayerLoseState(_system));
            yield break;
        }
        for (int i = 0; i < eligibleTargets.Count;i++)
        {
            if(!(attacker.getRow()-selectedMove.Distance <= eligibleTargets[i].getRow() && attacker.getRow() + selectedMove.Distance >= eligibleTargets[i].getRow()))
            {
                eligibleTargets.RemoveAt(i);
            }
            if (eligibleTargets.Count<=0)
            {
                _system.SetState(new AttackSetupState(_system));
                yield break;
            }
        }

        int index = Random.Range(0, eligibleTargets.Count);
        var target = eligibleTargets[index];

        // TODO: Replace with UI Text
        Debug.Log($"{attacker.Name} Attacked {target.Name}!");
        yield return new WaitForSeconds(_system.Delay);

        target.TakeDamage(selectedMove.Damage,selectedMove.Type);
        
        // if (move.Row)
        // {
        //     for (int i = 0; i < _system.AllBattlers.Count; i++)
        //     {
        //         if (_system.AllBattlers[i].getRow() == target.getRow())
        //         {
        //             if ((_system.AllBattlers[i] != target) && (_system.AllBattlers[i] != _system.ActiveBattler))
        //             {
        //                 bool Faint = _system.AllBattlers[i].TakeDamage(move.Damage, move.Type);
        //                 //if (Faint)
        //                 //{
        //                     //EventBus.Raise(new TargetFaintedEvent { target = _system.AllBattlers[i] });
        //                 //}
        //                 _system.AllBattlers[i].Heal(move.Healing);
        //             }
        //         }
        //     }
        // }

        _system.SetState(new AttackSetupState(_system));
    }
}