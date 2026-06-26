using UnityEngine;

public class GetBattleState : BattleState
{
    public GetBattleState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        if (_system.isSideFainted(Team.PLAYER))
        {
            // Move to player lost state
            Debug.Log("Player Lost");
            return;
        }
        if (_system.isSideFainted(Team.OPPONENT))
        {
            // Move to player lost state
            Debug.Log("Player Won!");
            return;
        }

        if (_system.AttackPhaseComplete && _system.MovePhaseComplete)
        {
            Debug.Log("Entering New Battle Phase");
            _system.UpdateAttackPhaseFlag(false);
            _system.UpdateMovePhaseFlag(false);
            _system.SetState(new InitiativeCheckState(_system));
        }

    }
}