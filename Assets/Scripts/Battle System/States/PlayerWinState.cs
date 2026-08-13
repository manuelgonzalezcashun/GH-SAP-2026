using System.Collections;
using UnityEngine;

public class PlayerWinState : BattleState
{
    public PlayerWinState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        // Win Conditions

        // Initiate End Battle Sequence
        _system.EndBattle();
    }
}
