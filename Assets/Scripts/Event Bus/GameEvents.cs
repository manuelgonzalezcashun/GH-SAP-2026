// * BO = Battle Options, ZO = Zone Options, MO = Move Options
using System.Collections.Generic;
using CraftingSystem;
using InventorySystem;
using UnityEngine;

public class PlayerHideEvent
{
    public bool _hidingMode = false;
}
public class SceneTransition
{
    public float _X = 0;
    public float _Y = 0;
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
public class InteractionWithinRangeEvent
{
    public bool enableIcon = false;
}

public class SetCameraBoundsEvent
{
    public Vector2 camBounds = Vector2.zero;
    public Vector3 camPos = Vector3.zero;
}
public class AddItemEvent
{
    public SO_Item item;
}

public class RemoveItemEvent
{
    public SO_Item item;
}

public class GetKeyEvent
{
    public List<string> PlayerKeys;
}
public class AddKeyEvent
{
    public string AddedKey;
}