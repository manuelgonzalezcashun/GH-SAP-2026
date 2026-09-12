using System.Collections;
using UnityEngine;

public class PlayerTurnState : BattleState
{
    public PlayerTurnState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        EventBus.Raise(new ShowOptionsEvent { BO_Show = true });
    }
    public override IEnumerator Attack(Move move)
    {
        _system.SetState(new PlayerAttackState(_system, move));
        yield return null;
    }
    public override IEnumerator Heal()
    {
        EventBus.Raise(new ShowOptionsEvent { BO_Show = false });
        yield return null;

        var healer = _system.ActiveBattler;

        // TODO: replace with UI text
        // Debug.Log($"{healer.Name} is recovering strength!");

        // healer.Heal(healer.Healing);
        yield return new WaitForSeconds(_system.Delay);

        BattleState state = new AttackSetupState(_system);
        _system.SetState(state);
    }
    public override IEnumerator Pass()
    {
        EventBus.Raise(new DisplayBattleTurnEvent { currentBattler = _system.ActiveBattler, isCurrentTurn = false });
        EventBus.Raise(new ShowOptionsEvent { BO_Show = false });
        yield return null;

        var attacker = _system.ActiveBattler;
        string displayText = $"{attacker.DisplayName} decided to pass his turn!";
        EventBus.Raise(new DisplayBattleTextEvent { battleText = displayText });
        yield return new WaitForSeconds(_system.Delay);

        _system.SetState(new AttackSetupState(_system));
    }
    public override IEnumerator Run()
    {
        EventBus.Raise(new DisplayBattleTurnEvent { currentBattler = _system.ActiveBattler, isCurrentTurn = false });
        EventBus.Raise(new ShowOptionsEvent { BO_Show = false });
        yield return null;

        int escapeChance = Random.Range(0, 10);
        if (escapeChance > 5)
        {
            string displayLine = $"{_system.ActiveBattler.DisplayName} was able to find an opening!";
            EventBus.Raise(new DisplayBattleTextEvent { battleText = displayLine });

            yield return new WaitForSeconds(_system.Delay);
            _system.SetState(new PlayerEscapeState(_system));
        }
        else
        {
            string displayLine = $"{_system.ActiveBattler.DisplayName} couldn't escape!";
            EventBus.Raise(new DisplayBattleTextEvent { battleText = displayLine });

            yield return new WaitForSeconds(_system.Delay);
            _system.SetState(new AttackSetupState(_system));
        }
    }
}