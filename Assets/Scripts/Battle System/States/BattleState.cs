using System.Collections;

public abstract class BattleState
{
    protected BattleSystem _system;
    public BattleState(BattleSystem system)
    {
        _system = system;
    }

    public abstract void EnterState();
    public virtual IEnumerator Attack()
    {
        yield break;
    }
    public virtual IEnumerator Heal()
    {
        yield break;
    }
    public virtual IEnumerator Items()
    {
        yield break;
    }
    public virtual IEnumerator Run()
    {
        yield break;
    }
}
