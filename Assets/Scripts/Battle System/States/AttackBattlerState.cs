using System.Collections;
using UnityEngine;

public class AttackBattlerState : BattleState
{
    public AttackBattlerState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        _system.StartCoroutine(Attack());
    }

    public override IEnumerator Attack()
    {
        yield return new WaitForSeconds(_system.Delay);
        Debug.Log($"{_system.ActiveBattler.Name} Attacked!");
        _system.SetState(new AttackSetupState(_system));
    }
}
