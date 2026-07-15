using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerAttackState : BattleState
{
    public PlayerAttackState(BattleSystem system, Move move) : base(system) { currentMove = move; }

    List<Battler> eligibleTargets => _system.AllBattlers.Where(battler => battler.Health > 0).ToList();
    List<Battler> eligibleEnemies => _system.AllBattlers.Where(battler => battler.Team == Team.OPPONENT && battler.Health > 0).ToList();
    List<Battler> eligibleAllies => _system.AllBattlers.Where(battler => battler.Team == Team.PLAYER && battler.Health > 0).ToList();
    Battler _target = null;
    int _selectedIndex = 0;
    Move currentMove = null;

    public override void EnterState()
    {
        EventBus.Raise(new ShowOptionsEvent { BO_Show = false });
        _target = null;
        _selectedIndex = 0;
    }
    public override void UpdateState()
    {
        if (_target != null) return;

        if (eligibleTargets.Count <= 0)
        {
            _system.SetState(new PlayerWinState(_system));
            return;
        }
        var targets = currentMove.AlliesAffected ? eligibleTargets : eligibleEnemies;
        if (targets.Count<=0)
        {
            _system.SetState(new AttackSetupState(_system));
            return;
        }
        for (int i = 0; i < targets.Count;i++)
        {
            if(!(_system.ActiveBattler.getRow()-currentMove.Distance <= targets[i].getRow() && _system.ActiveBattler.getRow() + currentMove.Distance >= targets[i].getRow()))
            {
                targets.RemoveAt(i);
            }
            if (targets.Count<=0)
            {
                _system.SetState(new AttackSetupState(_system));
                return;
            }
        }
        SelectTarget(targets);
    }

    private void SelectTarget(List<Battler> battlers)
    {
        EventBus.Raise(new SelectTargetEvent { _Target = battlers[_selectedIndex] });

        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            _selectedIndex--;
            if (_selectedIndex < 0) _selectedIndex = battlers.Count - 1;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            _selectedIndex++;
            if (_selectedIndex >= battlers.Count) _selectedIndex = 0;
        }
        else if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            _target = battlers[_selectedIndex];
            EventBus.Raise(new SelectTargetEvent { _Target = null });

            if (currentMove.Category == MoveCategory.DAMAGING)
                _system.StartCoroutine(Attack(currentMove));
            else
                _system.StartCoroutine(Heal());
        }
    }

    public override IEnumerator Attack(Move move)
    {
        var attacker = _system.ActiveBattler;
        bool targetFainted = _target.TakeDamage(move.Damage);

        if (move.Row)
        {
            for (int i = 0; i < eligibleTargets.Count; i++)
            {
                if (eligibleTargets[i].getRow() == _target.getRow())
                {
                    if ((eligibleTargets[i] != _target) && (eligibleTargets[i] != _system.ActiveBattler))
                    {
                        eligibleTargets[i].TakeDamage(move.Damage);
                    }
                }
            }
        }
    
        if (targetFainted)
        {
            EventBus.Raise(new TargetFaintedEvent { _Target = _target });
        }

        // TODO: Replace with UI Text
        Debug.Log($"{attacker.Name} Attacked {_target.Name}!");
        yield return new WaitForSeconds(_system.Delay);

        if (eligibleEnemies.Count <= 0)
        {
            _system.SetState(new PlayerWinState(_system));
            yield break;
        }

        _system.SetState(new AttackSetupState(_system));
    }
    public override IEnumerator Heal()
    {
        EventBus.Raise(new ShowOptionsEvent { BO_Show = false });

        var healer = _system.ActiveBattler;

        // TODO: replace with UI text
        // Debug.Log($"{healer.Name} is recovering strength!");

        _target.Heal(currentMove.Healing);
        yield return new WaitForSeconds(_system.Delay);

        BattleState state = new AttackSetupState(_system);
        _system.SetState(state);
    }
}
