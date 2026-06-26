
using Unity.VisualScripting;

public class InitiativeCheckState : BattleState
{
    public InitiativeCheckState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        _system.SetupTurnQueue();

        if (_system.MovePhaseComplete && !_system.AttackPhaseComplete)
        {
            _system.SetState(new AttackSetupState(_system));
            return;
        }

        _system.SetState(new MoveSetupState(_system));
    }
}