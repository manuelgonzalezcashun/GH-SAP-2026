using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiBattleUIHandler : MonoBehaviour
{
    [SerializeField] Transform playerHUDContainer = null;
    [SerializeField] Transform opponentHUDContainer = null;
    [SerializeField] GameObject zoneOptionsContainer = null;
    [SerializeField] BattleHUD hudPrefab = null;

    ZoneButton[] zoneButtons;
    void OnEnable()
    {
        BattleEvents.onSetupBattle += SetupBattleUI;
        BattleEvents.onShowZoneOptions += ShowZoneOptions;
    }
    void OnDisable()
    {
        BattleEvents.onSetupBattle -= SetupBattleUI;
        BattleEvents.onShowZoneOptions -= ShowZoneOptions;
    }
    void Awake()
    {
        zoneButtons = zoneOptionsContainer.GetComponentsInChildren<ZoneButton>();
    }

    private void ShowZoneOptions(Battler battler)
    {
        foreach (var button in zoneButtons)
        {
            button.SetActiveBattler(battler);
        }

        zoneOptionsContainer.SetActive(true);
    }

    void SetupBattleUI(Battler battler)
    {
        var hudParent = (battler.Team == Team.PLAYER)
        ? playerHUDContainer
        : opponentHUDContainer;

        var hud = Instantiate(hudPrefab, hudParent).GetComponent<BattleHUD>();
        hud.SetupBattleHUD(battler);
    }
}