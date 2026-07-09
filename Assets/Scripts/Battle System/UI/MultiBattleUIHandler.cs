using System;
using System.Collections.Generic;
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
        moveOptionsContainer.SetActive(data.MO_Show);
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