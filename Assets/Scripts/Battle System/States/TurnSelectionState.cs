using UnityEngine;

public class TurnSelectionState : BattleState
{
    public TurnSelectionState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        if (_system.TurnQueue.Count <= 0)
        {
            _system.SetupTurnQueue();
        }

        var activeBattler = _system.TurnQueue.Dequeue();

        if (activeBattler.Health <= 0)
        {
            _system.SetState(new TurnSelectionState(_system));
            return;
        }
        _system.SetActiveBattler(activeBattler);
        _system.SetState(new MoveBattlerState(_system));
    }

}
