// * BO = Battle Options, ZO = Zone Options, MO = Move Options
using CraftingSystem;
using InventorySystem;
using UnityEngine;

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

public class PlayerHideEvent
{
    public bool _hidingMode = false;
}
public class SceneTransition
{
    public float _X = 0;
    public float _Y = 0;
}
public class MoveSelectedEvent
{
    public Move move = null;
}
public class InitiateDialogueEvent
{
    public string knotName = string.Empty;
    public bool dialoguePlaying = false;
}
// TODO: Workshop name, this is to control whether the player can move
public class PlayerMoveEvent
{
    public bool canMove = true;
}

public class ItemSearchEvent
{
    public float _interactDistance = 0f;
    public Vector2 _interactPosition = Vector2.zero;
}
public class PlayerInteractEvent { }

public class EnterBattleEvent
{
    public TrainerParty Player = null;
    public TrainerParty Opponent = null;
}
public class SetCameraBoundsEvent
{
    public Vector2 camBounds = Vector2.zero;
    public Vector3 camPos = Vector3.zero;
}

public class DisplayBattleTextEvent
{
    public string battleText = string.Empty;
}

public class AddItemEvent
{
    public SO_Item item;
}

public class RemoveItemEvent
{
    public SO_Item item;
}
public class DisplayBattleTurnEvent
{
    public Battler currentBattler = null;
    public bool isCurrentTurn = false;
}

