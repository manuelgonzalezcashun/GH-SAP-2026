using System.Collections;
using UnityEngine;

public class MoveBattlerState : BattleState
{
    public MoveBattlerState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        _system.StartCoroutine(Move());
    }

    public override IEnumerator Move()
    {
        bool hasMoved = false;
        void onMoveCompletedHandler(OnMoveZoneEvent data)
        {
            hasMoved = true;
            EventBus.UnSubscribe<OnMoveZoneEvent>(onMoveCompletedHandler);
        }
        EventBus.Subscribe<OnMoveZoneEvent>(onMoveCompletedHandler);

        if (_system.ActiveBattler.Team == Team.PLAYER)
        {
            EventBus.Raise(new ShowOptionsEvent { ZO_Battler = _system.ActiveBattler });
        }
        else
        {
            int randomStep = Random.Range(-1, 2);
            EventBus.Raise(new OnMoveZoneEvent { _Battler = _system.ActiveBattler, _ZoneStep = randomStep });
        }
        TickStack();
        EventBus.Raise(new DisplayBattleTextEvent { battleText = $"{_system.ActiveBattler.DisplayName} Decided Where to Move" });
        yield return new WaitUntil(() => hasMoved);

        EventBus.Raise(new DisplayBattleTurnEvent { currentBattler = _system.ActiveBattler, isCurrentTurn = false });
        yield return new WaitForSeconds(_system.Delay);
        _system.SetState(new MoveSetupState(_system));
    }
    public void TickStack()
    {
        if (_system.ActiveBattler.Poison_STK > 0)
        {
            _system.ActiveBattler.ChangeStack(-1,Stack.POISON);
            _system.ActiveBattler.TakeDamage(1,Type.NONE);
            _system.ActiveBattler.ChangeStack(1,Stack.HOPE);
            Debug.Log(_system.ActiveBattler+"\nArmor: " + _system.ActiveBattler.Armor_STK+"\nPoison: "+_system.ActiveBattler.Poison_STK);
        }
    }


}
