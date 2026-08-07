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
        if (eligibleTargets.Count <= 0) // If all Player Battlers fainted, Player has lost 
        {
            _system.SetState(new PlayerLoseState(_system));
            yield break;
        }

        eligibleTargets.RemoveAll(battler => !(attacker.getRow() - selectedMove.Distance <= battler.getRow()) && attacker.getRow() + selectedMove.Distance >= battler.getRow());
        if (eligibleTargets.Count <= 0) // Targets are out of range
        {
            _system.SetState(new AttackSetupState(_system));
            yield break;
        }

        int index = Random.Range(0, eligibleTargets.Count);
        var target = eligibleTargets[index];

        EventBus.Raise(new DisplayBattleTextEvent { battleText = $"{attacker.Name} Attacked {target.Name} with {selectedMove.Name}!" });
        yield return new WaitForSeconds(_system.Delay);

        bool targetFainted = target.TakeDamage(selectedMove.Damage, selectedMove.Type);

        if (targetFainted)
            EventBus.Raise(new TargetFaintedEvent { _Target = target });

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