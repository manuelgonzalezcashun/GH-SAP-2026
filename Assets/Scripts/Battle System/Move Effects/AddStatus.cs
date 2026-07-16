using UnityEngine;

public class AddStatus : MoveEffects
{
    public override void OnAttack()
    {
        changeStatus();
    }

    public virtual void changeStatus()
    {
        
    }
}
