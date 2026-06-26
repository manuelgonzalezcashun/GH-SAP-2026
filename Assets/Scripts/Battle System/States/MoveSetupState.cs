using UnityEngine;

public class MoveSetupState : BattleState
{
    public MoveSetupState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        if (_system.TurnQueue.Count <= 0)
        {
            _system.UpdateMovePhaseFlag(true);
            _system.SetState(new InitiativeCheckState(_system));
            return;
        }

        var activeBattler = _system.TurnQueue.Dequeue();

        if (activeBattler.Health <= 0)
        {
            _system.SetState(new MoveSetupState(_system));
            return;
        }

        _system.SetActiveBattler(activeBattler);
        _system.SetState(new MoveBattlerState(_system));
    }

}
