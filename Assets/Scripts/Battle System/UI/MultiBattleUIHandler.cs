using System.Collections.Generic;
using UnityEngine;

public class MultiBattleUIHandler : MonoBehaviour
{
    [SerializeField] Transform playerHUDContainer = null;
    [SerializeField] Transform opponentHUDContainer = null;
    [SerializeField] BattleHUD hudPrefab = null;


    void OnEnable()
    {
        BattleEvents.onSetupBattle += SetupBattleUI;
    }
    void OnDisable()
    {
        BattleEvents.onSetupBattle -= SetupBattleUI;
    }

    void SetupBattleUI(Battler battler)
    {
        var hudParent = (battler.Faction == Faction.PLAYER)
        ? playerHUDContainer
        : opponentHUDContainer;

        var hud = Instantiate(hudPrefab, hudParent).GetComponent<BattleHUD>();
        hud.SetupBattleHUD(battler);
    }
}