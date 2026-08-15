using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    #region Singleton Code
    private static InputHandler _instance = null;
    public static InputHandler Instance => _instance;
    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(Instance);
    }
    #endregion
    [SerializeField] InputActionAsset inputActions;
    private InputActionMap currentActionMap = null;

    // Player Actions
    InputAction MoveAction => inputActions["Move"];
    InputAction InteractAction => inputActions["Interact"];
    InputAction ContinueDialogueAction => inputActions["ContinueDialogue"];
    InputAction SelectRightAction => inputActions["SelectRight"];
    InputAction SelectLeftAction => inputActions["SelectLeft"];
    InputAction ConfirmTargetAction => inputActions["ConfirmTarget"];
    InputAction SelectUpAction => inputActions["SelectUp"];
    InputAction SelectDownAction => inputActions["SelectDown"];
    InputAction EscapeAction => inputActions["ExitPanel"];
    InputAction EnableCraftingAction => inputActions["EnableCraftingMenu"];
    InputAction PauseAction => inputActions["PauseGame"];
    // ACTION MAP INDICES //
    public static int playerInput => 0;
    public static int mainMenuInput => 1;
    public static int dialogueInput => 2;
    public static int combatInput => 3;

    // Input Events
    public static bool SubmitPressed => _instance.ContinueDialogueAction.WasPressedThisFrame();
    public static bool InteractPressed => _instance.InteractAction.WasPressedThisFrame();
    public static Vector2 Movement => _instance.MoveAction.ReadValue<Vector2>();
    public static bool SelectedRightButton => _instance.SelectRightAction.WasPressedThisFrame();
    public static bool SelectedLeftButton => _instance.SelectLeftAction.WasPressedThisFrame();
    public static bool ConfirmTargetPressed => _instance.ConfirmTargetAction.WasPressedThisFrame();
    public static bool MainMenuSelectUp => _instance.SelectUpAction.WasPerformedThisFrame();
    public static bool MainMenuSelectDown => _instance.SelectDownAction.WasPerformedThisFrame();
    public static bool ExitPanelPressed => _instance.EscapeAction.WasPressedThisFrame();
    public static bool EnableCraftingMenuPressed => _instance.EnableCraftingAction.WasPerformedThisFrame();
    public static bool CursorToggleEnabled => _instance.cursorToggle;
    public static bool PauseGamePressed => _instance.PauseAction.WasPressedThisFrame();
    void Update()
    {
        CursorToggleMode();
    }

    private bool cursorToggle = false;
    void CursorToggleMode()
    {
        bool isMoving = Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f;

        if (isMoving) cursorToggle = true;
        if (MainMenuSelectUp || MainMenuSelectDown) cursorToggle = false;
        if (SelectedLeftButton || SelectedRightButton) cursorToggle = false;
    }
    void _ChangeActionMaps(int actionMapIndex)
    {
        currentActionMap = inputActions.actionMaps[actionMapIndex];
        inputActions.Disable();
        currentActionMap.Enable();
    }
    public static void ChangeActionMaps(int actionMapIndex)
    {
        _instance._ChangeActionMaps(actionMapIndex);
    }

}
