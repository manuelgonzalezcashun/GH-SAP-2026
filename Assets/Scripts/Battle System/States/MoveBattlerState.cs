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
        void onMoveCompletedHandler()
        {
            hasMoved = true;
            BattleEvents.onMoveCompleted -= onMoveCompletedHandler;
        }
        BattleEvents.onMoveCompleted += onMoveCompletedHandler;

        if (_system.ActiveBattler.Faction == Faction.PLAYER)
        {
            BattleEvents.ShowZoneOptions(_system.ActiveBattler);
        }
        else
        {
            int index = Random.Range(0, _system.Zones.Length);
            Zone randZone = _system.Zones[index];
            BattleEvents.SetBattlerInZone(_system.ActiveBattler, randZone);
            BattleEvents.MoveComplete();
        }

        yield return new WaitUntil(() => hasMoved);
        yield return new WaitForSeconds(_system.Delay);

        _system.SetState(new TurnSelectionState(_system));
    }


}
