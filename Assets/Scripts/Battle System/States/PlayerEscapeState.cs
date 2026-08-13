public class PlayerEscapeState : BattleState
{
    public PlayerEscapeState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        // Player Escape Conditions

        // Initial End Battle Sequence
        _system.EndBattle();
    }
}
