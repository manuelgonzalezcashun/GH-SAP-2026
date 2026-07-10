using System;
using System.Collections.Generic;
using System.Linq;
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

    ZoneButton[] zoneButtons;
    void OnEnable()
    {
        EventBus.Subscribe<SetupBattleEvent>(SetupBattleUI);
        EventBus.Subscribe<ShowOptionsEvent>(ShowZoneOptions);
        EventBus.Subscribe<ShowOptionsEvent>(ShowBattleOptions);
        EventBus.Subscribe<ShowOptionsEvent>(ShowMoveOptions);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<SetupBattleEvent>(SetupBattleUI);
        EventBus.UnSubscribe<ShowOptionsEvent>(ShowZoneOptions);
        EventBus.UnSubscribe<ShowOptionsEvent>(ShowBattleOptions);
        EventBus.UnSubscribe<ShowOptionsEvent>(ShowMoveOptions);
    }

    private void ShowBattleOptions(ShowOptionsEvent data)
    {
        battleOptionContainer.SetActive(data.BO_Show);
    }
    private void ShowMoveOptions(ShowOptionsEvent data)
    {
        foreach (var move in moveOptions)
            move.onClick.RemoveAllListeners();

        moveOptionsContainer.SetActive(data.MO_Show);

        if (data.MO_Battler != null)
            SetupMoves(data.MO_Battler);
    }

    private void SetupMoves(Battler battler)
    {
        int moveCount = battler.Moves.Length;

        for (int i = 0; i < moveOptions.Length; i++)
        {
            if (i >= moveCount) return;

            moveOptions[i].gameObject.SetActive(i < moveCount);
            TMP_Text moveText = moveOptions[i].GetComponentInChildren<TMP_Text>();
            moveText.text = battler.Moves[i].Name;

            MoveSelectedEvent evtData = new MoveSelectedEvent { move = battler.Moves[i] };
            moveOptions[i].onClick.AddListener(() => EventBus.Raise(evtData));
            moveOptions[i].onClick.AddListener(() => EventBus.Raise(new ShowOptionsEvent { MO_Show = false, MO_Battler = null }));
        }
    }

    void Awake()
    {
        zoneButtons = zoneOptionsContainer.GetComponentsInChildren<ZoneButton>();
    }

    private void ShowZoneOptions(ShowOptionsEvent data)
    {
        if (data.ZO_Battler == null) return;

        foreach (var button in zoneButtons)
        {
            button.SetActiveBattler(data.ZO_Battler);
        }

        zoneOptionsContainer.SetActive(true);
        data.ZO_Battler = null;
    }

    void SetupBattleUI(SetupBattleEvent data)
    {
        var hudParent = (data._Battler.Team == Team.PLAYER)
        ? playerHUDContainer
        : opponentHUDContainer;

        var hud = Instantiate(hudPrefab, hudParent).GetComponent<BattleHUD>();
        hud.SetupBattleHUD(data._Battler);
    }
}