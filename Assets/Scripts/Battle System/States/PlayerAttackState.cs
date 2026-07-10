using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerAttackState : BattleState
{
    public PlayerAttackState(BattleSystem system, Move move) : base(system) { currentMove = move; }

    List<Battler> eligibleTargets => _system.AllBattlers.Where(battler => battler.Team == Team.OPPONENT && battler.Health > 0).ToList();
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

        EventBus.Raise(new SelectTargetEvent { _Target = eligibleTargets[_selectedIndex] });

        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            _selectedIndex--;
            if (_selectedIndex < 0) _selectedIndex = eligibleTargets.Count - 1;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            _selectedIndex++;
            if (_selectedIndex >= eligibleTargets.Count) _selectedIndex = 0;
        }
        else if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            _target = eligibleTargets[_selectedIndex];
            EventBus.Raise(new SelectTargetEvent { _Target = null });
            _system.StartCoroutine(Attack(currentMove));
        }
    }
    public override IEnumerator Attack(Move move)
    {
        var attacker = _system.ActiveBattler;
        bool targetFainted = _target.TakeDamage(move.Damage);

        if (targetFainted)
        {
            EventBus.Raise(new TargetFaintedEvent { _Target = _target });
        }

        // TODO: Replace with UI Text
        // Debug.Log($"{attacker.Name} Attacked {_target.Name}!");
        yield return new WaitForSeconds(_system.Delay);

        if (eligibleTargets.Count <= 0)
        {
            _system.SetState(new PlayerWinState(_system));
            yield break;
        }

        _system.SetState(new AttackSetupState(_system));
    }
}
