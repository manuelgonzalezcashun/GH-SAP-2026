using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleEvents
{
    public static event Action<Battler, Zone> onBattleZoneChanged;
    public static event Action onBattleEnded;
    public static event Action<bool> onShowOptions;
    public static event Action<Battler> onSetupBattle;
    public static event Action<Battler> onBroadcastActiveBattler;

    public static void ShowBattleOptions(bool show)
    {
        onShowOptions?.Invoke(show);
    }
    public static void EndBattle()
    {
        onBattleEnded?.Invoke();
    }
    public static void SetBattlerInZone(Battler _battler, Zone _zone)
    {
        onBattleZoneChanged?.Invoke(_battler, _zone);
    }
    public static void SetupBattle(Battler battler)
    {
        onSetupBattle?.Invoke(battler);
    }
    public static void BroadcastActiveBattler(Battler battler)
    {
        onBroadcastActiveBattler?.Invoke(battler);
    }
}
