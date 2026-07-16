using UnityEngine;

public abstract class ChangeRow : MoveEffects
{
    public override void OnAttack()
    {
        changeRow();
    }

    public virtual void changeRow()
    {
        
    }
}
