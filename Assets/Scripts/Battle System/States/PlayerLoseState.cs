using System.Collections;
using UnityEngine;

public class PlayerLoseState : BattleState
{
    public PlayerLoseState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        // Player Lose Conditions

        // Initial End Battle Sequence
        _system.EndBattle();
        //GameManager.Instance.SetState(new InMenuState());
        //GameManager.Instance.LoadScene();
    }
}