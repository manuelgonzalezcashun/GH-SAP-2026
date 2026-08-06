public abstract class State
{
    public abstract void EnterState();
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}