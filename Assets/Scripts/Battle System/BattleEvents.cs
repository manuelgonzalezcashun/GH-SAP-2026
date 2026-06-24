using System;
using UnityEngine;

public static class BattleEvents
{
    public static event Action<Battler, Battler> onBattleStarted;
    public static event Action<Battler, Zone> onBattlerMove;
    public static event Action onBattleEnded;
    public static event Action<bool> onShowOptions;

    public static void ShowBattleOptions(bool show)
    {
        onShowOptions?.Invoke(show);
    }
    public static void EndBattle()
    {
        onBattleEnded?.Invoke();
    }
    public static void StartBattle(Battler player, Battler opponent)
    {
        onBattleStarted?.Invoke(player, opponent);
    }
    public static void SetBattlerInZone(Battler _battler, Zone _zone)
    {
        onBattlerMove?.Invoke(_battler, _zone);
    }
}
