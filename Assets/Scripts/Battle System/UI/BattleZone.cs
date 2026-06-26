using System.Collections.Generic;
using UnityEngine;

public enum Zone
{ P_BACK, P_FRONT, O_BACK, O_FRONT }
public class BattleZone : MonoBehaviour
{
    const int ZONE_SIZE = 20;
    Dictionary<Battler, BattleUnit> activeBattleUnits = new Dictionary<Battler, BattleUnit>();
    Queue<BattleUnit> unitPool = new Queue<BattleUnit>();

    [SerializeField] Zone zone;
    [SerializeField] BattleUnit unitPrefab;

    void OnEnable()
    {
        BattleEvents.onBattleZoneChanged += SetBattlerInZone;
    }

    void OnDisable()
    {
        BattleEvents.onBattleZoneChanged -= SetBattlerInZone;
    }

    void SetBattlerInZone(Battler battler, Zone zone)
    {
        if (this.zone == zone && activeBattleUnits.ContainsKey(battler)) return;

        if (CanMoveToThisZone(battler, zone))
        {
            BattleUnit _battleUnit = GetBattleUnit();
            _battleUnit.SetBattlerInUnit(battler);
            activeBattleUnits.Add(battler, _battleUnit);
        }
        else if (activeBattleUnits.TryGetValue(battler, out BattleUnit unit))
        {
            ClearBattleUnit(unit);
            activeBattleUnits.Remove(battler);
        }
    }

    private BattleUnit GetBattleUnit()
    {
        BattleUnit _battleUnit = unitPool.Count > 0 ? unitPool.Dequeue() : Instantiate(unitPrefab, transform);
        _battleUnit.gameObject.SetActive(true);
        return _battleUnit;
    }
    private void ClearBattleUnit(BattleUnit unit)
    {
        unit.ClearUnit();
        unitPool.Enqueue(unit);
    }

    private bool CanMoveToThisZone(Battler battler, Zone zone)
    {
        if (this.zone != zone) return false; // Ignores Battler if not set to this zone
        if (activeBattleUnits.Count >= ZONE_SIZE) return false; // Too many battlers
        if (activeBattleUnits.ContainsKey(battler)) return false; // Battler already exists in this zone

        return true;
    }
}
