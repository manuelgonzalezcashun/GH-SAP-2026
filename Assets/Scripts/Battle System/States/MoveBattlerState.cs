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
        void onMoveCompletedHandler(OnMoveEvent data)
        {
            hasMoved = true;
            EventBus.UnSubscribe<OnMoveEvent>(onMoveCompletedHandler);
        }
        EventBus.Subscribe<OnMoveEvent>(onMoveCompletedHandler);

        if (_system.ActiveBattler.Team == Team.PLAYER)
        {
            EventBus.Raise(new ShowOptionsEvent { ZO_Battler = _system.ActiveBattler });
        }
        else
        {
            int index = Random.Range(0, _system.Zones.Length);
            Zone randZone = _system.Zones[index];
            EventBus.Raise(new OnMoveEvent { _Battler = _system.ActiveBattler, _Zone = randZone });
        }

        yield return new WaitUntil(() => hasMoved);
        yield return new WaitForSeconds(_system.Delay);

        _system.SetState(new MoveSetupState(_system));
    }


}
