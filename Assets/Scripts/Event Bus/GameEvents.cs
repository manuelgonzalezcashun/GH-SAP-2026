// * BO = Battle Options, ZO = Zone Options
public class ShowOptionsEvent
{
    public bool BO_Show = false;
    public Battler ZO_Battler = null;
}

public class EndBattleEvent { }

public class OnMoveEvent
{
    public Battler _Battler = null;
    public Zone _Zone;
}

public class SetupBattleEvent
{
    public Battler _Battler = null;
    public Zone _Zone;
}

public class SelectTargetEvent
{
    public Battler _Target = null;
}
public class TargetFaintedEvent
{
    public Battler _Target = null;
}

