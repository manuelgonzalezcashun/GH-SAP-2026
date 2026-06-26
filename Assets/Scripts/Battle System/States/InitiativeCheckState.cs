
public class InitiativeCheckState : BattleState
{
    public InitiativeCheckState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        _system.SetupTurnQueue();
        _system.SetState(new TurnSelectionState(_system));
    }
}
