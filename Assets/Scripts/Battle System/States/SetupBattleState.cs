using System.Collections;
using System.Collections.Generic;

public class SetupBattleState : BattleState
{
    public SetupBattleState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        _system.SetupBattle();
        _system.SetState(new InitiativeCheckState(_system));
    }
}
