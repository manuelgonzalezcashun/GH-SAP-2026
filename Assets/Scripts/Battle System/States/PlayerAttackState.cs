using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class PlayerAttackState : BattleState
{
    public PlayerAttackState(BattleSystem system) : base(system) { }

    Battler _target = null;
    int _selectedIndex = 0;

    public override void EnterState()
    {
        BattleEvents.ShowBattleOptions(false);
        _target = null;
        _selectedIndex = 0;
    }
    public override void UpdateState()
    {
        if (_target != null) return;

        var eligibleTargets = _system.AllBattlers.Where(battler => battler.Team == Team.OPPONENT && battler.Health > 0).ToList();

        if (eligibleTargets.Count <= 0) return;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            _selectedIndex--;
            if (_selectedIndex < 0) _selectedIndex = eligibleTargets.Count - 1;
            BattleEvents.SelectTargetBattler(eligibleTargets[_selectedIndex]);
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            _selectedIndex++;
            if (_selectedIndex >= eligibleTargets.Count) _selectedIndex = 0;
            BattleEvents.SelectTargetBattler(eligibleTargets[_selectedIndex]);
        }
        else if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            _target = eligibleTargets[_selectedIndex];
            BattleEvents.SelectTargetBattler(null);
            _system.StartCoroutine(Attack());
        }
    }
    public override IEnumerator Attack()
    {
        var attacker = _system.ActiveBattler;

        _target.TakeDamage(attacker.Power);
        Debug.Log($"{attacker.Name} Attacked {_target.Name}!");

        yield return new WaitForSeconds(_system.Delay);
        _system.SetState(new AttackSetupState(_system));
    }
}
