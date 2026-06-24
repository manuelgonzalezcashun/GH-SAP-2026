using System.Collections.Generic;
using UnityEngine;

public enum Zone { P_BACKLINE, P_COMBAT, O_BACKLINE, O_COMBAT }
public class BattleZone : MonoBehaviour
{
    const int ZONE_SIZE = 20;
    Queue<BattleUnit> unitPool = new Queue<BattleUnit>();

    [SerializeField] Zone zone;
    [SerializeField] BattleUnit unitPrefab;
    private int _zoneCount = 0;

    void OnEnable()
    {
        BattleEvents.onBattlerMove += SetBattlerInZone;
    }

    void OnDisable()
    {
        BattleEvents.onBattlerMove -= SetBattlerInZone;
    }

    void SetBattlerInZone(Battler battler, Zone zone)
    {
        if (_zoneCount >= ZONE_SIZE) return; // Too many battlers
        if (this.zone != zone) return; // Ignores Battler if not set to this zone

        var _battleUnit = Instantiate(unitPrefab, transform);
        _battleUnit.SetBattlerInUnit(battler);
        unitPool.Enqueue(_battleUnit);
    }
}
