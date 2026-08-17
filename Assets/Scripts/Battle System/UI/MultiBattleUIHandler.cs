using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiBattleUIHandler : MonoBehaviour
{
    [SerializeField] Transform playerHUDContainer = null;
    [SerializeField] Transform opponentHUDContainer = null;
    [SerializeField] GameObject battleOptionContainer = null;
    [SerializeField] GameObject moveOptionsContainer = null;
    [SerializeField] GameObject zoneOptionsContainer = null;
    [SerializeField] BattleHUD hudPrefab = null;
    [SerializeField] Button[] moveOptions = null;
    [SerializeField] ZoneButton[] zoneButtons;
    [SerializeField] BattleButton[] battleOptions = null;
    [SerializeField] TMP_Text battleTextLabel = null;

    Dictionary<Battler, BattleHUD> activeBattleHUDs = new Dictionary<Battler, BattleHUD>();
    Queue<BattleHUD> hudPool = new Queue<BattleHUD>();
    int buttonIndex = 0;
    int moveCount = 0;
    Move[] currentMoves = null;
    void OnEnable()
    {
        EventBus.Subscribe<SetupBattleEvent>(SetupBattleUI);
        EventBus.Subscribe<ShowOptionsEvent>(ShowZoneOptions);
        EventBus.Subscribe<ShowOptionsEvent>(ShowBattleOptions);
        EventBus.Subscribe<ShowOptionsEvent>(ShowMoveOptions);
        EventBus.Subscribe<EndBattleEvent>(ClearBattleUI);
        EventBus.Subscribe<TargetFaintedEvent>(ClearBattleHUD);
        EventBus.Subscribe<DisplayBattleTextEvent>(DisplayText);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<SetupBattleEvent>(SetupBattleUI);
        EventBus.UnSubscribe<ShowOptionsEvent>(ShowZoneOptions);
        EventBus.UnSubscribe<ShowOptionsEvent>(ShowBattleOptions);
        EventBus.UnSubscribe<ShowOptionsEvent>(ShowMoveOptions);
        EventBus.UnSubscribe<EndBattleEvent>(ClearBattleUI);
        EventBus.UnSubscribe<TargetFaintedEvent>(ClearBattleHUD);
        EventBus.UnSubscribe<DisplayBattleTextEvent>(DisplayText);
    }


    private void ShowBattleOptions(ShowOptionsEvent data)
    {
        battleOptionContainer.SetActive(data.BO_Show);
        battleOptions[0].Select();
    }

    private void ShowMoveOptions(ShowOptionsEvent data)
    {
        // Makes sure each button is clear before setting up moves
        foreach (var move in moveOptions)
        {
            move.onClick.RemoveAllListeners();
            move.gameObject.SetActive(false);
        }

        moveOptionsContainer.SetActive(data.MO_Show);
        moveOptions[0].Select();
        if (data.MO_Battler != null) SetupMoves(data.MO_Battler);
    }

    private void SetupMoves(Battler battler)
    {
        currentMoves = battler.Moves;
        moveCount = currentMoves.Length;

        for (int i = 0; i < moveOptions.Length; i++)
        {
            if (i >= moveCount) return;

            // Sets up each button UI to contain move data
            moveOptions[i].gameObject.SetActive(i < moveCount);
            TMP_Text moveText = moveOptions[i].GetComponentInChildren<TMP_Text>();
            moveText.text = currentMoves[i].Name;

            // Sends Move data to battle System
            MoveSelectedEvent evtData = new MoveSelectedEvent { move = currentMoves[i] };
            moveOptions[i].onClick.AddListener(() => EventBus.Raise(evtData));
            moveOptions[i].onClick.AddListener(() => EventBus.Raise(new ShowOptionsEvent { MO_Show = false, MO_Battler = null }));
            moveOptions[i].onClick.AddListener(() => currentMoves = null);
            moveOptions[i].onClick.AddListener(() => EventBus.Raise(new DisplayBattleTextEvent { battleText = string.Empty }));
        }
    }

    void Awake()
    {
        battleTextLabel.text = string.Empty;
    }
    void Update()
    {
        ZoneButtonSelector();
        MoveButtonSelector();
        BattleOptionSelector();
    }
    private void ShowZoneOptions(ShowOptionsEvent data)
    {
        if (data.ZO_Battler == null) return;

        foreach (var button in zoneButtons)
        {
            button.SetActiveBattler(data.ZO_Battler);
        }

        zoneOptionsContainer.SetActive(true);
        zoneButtons[0].Select();
        data.ZO_Battler = null;
    }

    void SetupBattleUI(SetupBattleEvent data)
    {
        var hud = GetBattleHUD(data._Battler);
        hud.SetupBattleHUD(data._Battler);

        activeBattleHUDs[data._Battler] = hud;
    }
    private void ClearBattleUI(EndBattleEvent data)
    {
        EventBus.Raise(new ShowOptionsEvent { MO_Battler = null, MO_Show = false, BO_Show = false, ZO_Battler = null });
        ReturnToHudPool();
        activeBattleHUDs.Clear();

        foreach (var move in moveOptions)
        {
            move.onClick.RemoveAllListeners();
        }
        foreach (var zoneBtn in zoneButtons)
        {
            zoneBtn.SetActiveBattler(null);
        }
    }

    private void ReturnToHudPool()
    {
        BattleHUD[] playerHUDS = playerHUDContainer.GetComponentsInChildren<BattleHUD>();
        BattleHUD[] oppHUDS = opponentHUDContainer.GetComponentsInChildren<BattleHUD>();

        foreach (BattleHUD hud in playerHUDS)
        {
            hud.ClearBattleHUD();
            hudPool.Enqueue(hud);
        }
        foreach (BattleHUD hud in oppHUDS)
        {
            hud.ClearBattleHUD();
            hudPool.Enqueue(hud);
        }
    }
    private void ReturnToHudPool(Battler battler)
    {
        BattleHUD current = activeBattleHUDs[battler];
        current.ClearBattleHUD();
        hudPool.Enqueue(current);

        activeBattleHUDs.Remove(battler);
    }
    private void ClearBattleHUD(TargetFaintedEvent data)
    {
        ReturnToHudPool(data._Target);
    }
    private BattleHUD GetBattleHUD(Battler battler)
    {
        var hudParent = (battler.Team == Team.PLAYER)
        ? playerHUDContainer
        : opponentHUDContainer;

        BattleHUD battleHUD = hudPool.Count > 0
        ? hudPool.Dequeue()
        : Instantiate(hudPrefab, hudParent).GetComponent<BattleHUD>();

        battleHUD.transform.SetParent(hudParent);
        return battleHUD;
    }
    private void DisplayText(DisplayBattleTextEvent data)
    {
        battleTextLabel.text = data.battleText;
    }

    // Helper Methods
    private Button ButtonSelectionHandler(int length, Button[] buttons)
    {
        if (InputHandler.SelectedRightButton)
        {
            buttonIndex++;
            buttonIndex %= length;
        }

        if (InputHandler.SelectedLeftButton)
        {
            if (buttonIndex > 0)
                buttonIndex--;
            else
                buttonIndex = length - 1;
        }

        return buttons[buttonIndex];
    }
    private void ZoneButtonSelector()
    {
        if (!zoneOptionsContainer.activeSelf) return;

        ZoneButton currentButton = ButtonSelectionHandler(zoneButtons.Length, zoneButtons) as ZoneButton;
        currentButton.Select();
        EventBus.Raise(new DisplayBattleTextEvent { battleText = currentButton.Description });
    }
    private void MoveButtonSelector()
    {
        if (!moveOptionsContainer.activeSelf) return;

        Button currentButton = ButtonSelectionHandler(moveCount, moveOptions);
        currentButton.Select();

        EventBus.Raise(new DisplayBattleTextEvent { battleText = currentMoves[buttonIndex].Info });
    }
    private void BattleOptionSelector()
    {
        if (!battleOptionContainer.activeSelf) return;

        BattleButton currentButton = ButtonSelectionHandler(battleOptions.Length, battleOptions) as BattleButton;
        currentButton.Select();

        EventBus.Raise(new DisplayBattleTextEvent { battleText = currentButton.Description });
    }
}