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
        if (targets.Count <= 0)
        {
            _system.SetState(new AttackSetupState(_system));
            return;
        }

        targets.RemoveAll(battler => !(_system.ActiveBattler.getRow() - currentMove.Distance <= battler.getRow() && _system.ActiveBattler.getRow() + currentMove.Distance >= battler.getRow()));
        if (targets.Count <= 0)
        {
            _system.SetState(new AttackSetupState(_system));
            return;
        }

        if (currentMove.Distance == -1)
        {
            List<Battler> i = new List<Battler>();
            i.Add(_system.ActiveBattler);
            targets = i;
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
        bool targetFainted = _target.TakeDamage(move.Damage, move.Type);

        if (move.Row)
        {
            for (int i = 0; i < _system.AllBattlers.Count; i++)
            {
                if (_system.AllBattlers[i].getRow() == _target.getRow())
                {
                    if ((_system.AllBattlers[i] != _target) && (_system.AllBattlers[i] != _system.ActiveBattler))
                    {
                        bool Faint = _system.AllBattlers[i].TakeDamage(move.Damage, move.Type);
                        //if (Faint)
                        //{
                        //EventBus.Raise(new TargetFaintedEvent { _Target = _system.AllBattlers[i] });
                        //}
                        _system.AllBattlers[i].Heal(move.Healing);
                    }
                }
            }
        }

        if (targetFainted)
        {
            EventBus.Raise(new TargetFaintedEvent { _Target = _target });
        }

        EventBus.Raise(new DisplayBattleTextEvent { battleText = $"{attacker.Name} Attacked {_target.Name} with {move.Name}!" });
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

        // TODO: replace with UI text
        // Debug.Log($"{healer.Name} is recovering strength!");

        _target.Heal(currentMove.Healing);
        yield return new WaitForSeconds(_system.Delay);

        BattleState state = new AttackSetupState(_system);
        _system.SetState(state);
    }
}
