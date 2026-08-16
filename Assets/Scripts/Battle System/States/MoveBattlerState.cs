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
        EventBus.Raise(new DisplayBattleTextEvent { battleText = $"{_system.ActiveBattler.DisplayName} Decided Where to Move" });
        yield return new WaitUntil(() => hasMoved);

        EventBus.Raise(new DisplayBattleTurnEvent { currentBattler = _system.ActiveBattler, isCurrentTurn = false });
        yield return new WaitForSeconds(_system.Delay);
        _system.SetState(new MoveSetupState(_system));
    }


}
