using System.Collections;

public abstract class BattleState : State
{
    protected BattleSystem _system;
    public BattleState(BattleSystem system)
    {
        _system = system;
    }
    public virtual IEnumerator Attack(Move move)
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
    public virtual IEnumerator Move()
    {
        yield break;
    }
}
