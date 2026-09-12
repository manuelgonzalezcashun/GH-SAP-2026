public class ShowOptionsEvent
{
    public bool BO_Show = false;
    public bool MO_Show = false;
    public Battler ZO_Battler = null;
    public Battler MO_Battler = null;
}

public class EndBattleEvent { }

public class OnMoveZoneEvent
{
    public Battler _Battler = null;
    public int _ZoneStep = 0;
}
public class OnZoneSelectedEvent
{
    public Battler _Battler = null;
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
public class MoveSelectedEvent
{
    public Move move = null;
}
public class EnterBattleEvent
{
    public TrainerParty Player = null;
    public TrainerParty Opponent = null;
}
public class DisplayBattleTextEvent
{
    public string battleText = string.Empty;
}
public class DisplayBattleTurnEvent
{
    public Battler currentBattler = null;
    public bool isCurrentTurn = false;
}
public class DamageUnitEvent
{
    public Battler battler = null;
}