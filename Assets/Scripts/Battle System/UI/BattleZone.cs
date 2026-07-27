using System;
using System.Collections.Generic;
using UnityEngine;


public class BattleZone : MonoBehaviour
{
    const int ZONE_SIZE = 20;
    Dictionary<Battler, BattleUnit> activeBattleUnits = new Dictionary<Battler, BattleUnit>();
    Queue<BattleUnit> unitPool = new Queue<BattleUnit>();

    [SerializeField] Zone _zone;
    [SerializeField] BattleUnit unitPrefab;

    void OnEnable()
    {
        EventBus.Subscribe<OnZoneSelectedEvent>(ClearBattler);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<OnZoneSelectedEvent>(ClearBattler);
    }
    public void ClearBattler(OnZoneSelectedEvent data)
    {
        ClearBattler(data._Battler);
    }
    public void SetBattlerInZone(Battler battler, Zone zone)
    {
        if (CanMoveToThisZone(battler, zone))
        {
            BattleUnit _battleUnit = GetBattleUnit();
            _battleUnit.SetBattlerInUnit(battler);
            activeBattleUnits[battler] = _battleUnit;
            battler.setRow((int)zone);
        }
    }

    private BattleUnit GetBattleUnit()
    {
        BattleUnit _battleUnit = unitPool.Count > 0 ? unitPool.Dequeue() : Instantiate(unitPrefab, transform);
        _battleUnit.gameObject.SetActive(true);
        return _battleUnit;
    }
    private void ClearBattler(Battler battler)
    {
        if (activeBattleUnits.TryGetValue(battler, out BattleUnit unit))
        {
            unit.ClearUnit();
            unitPool.Enqueue(unit);
            activeBattleUnits.Remove(battler);
        }
    }
    private bool CanMoveToThisZone(Battler battler, Zone zone)
    {
        if (_zone != zone) return false; // Ignores Battler if not set to this zone
        if (activeBattleUnits.Count >= ZONE_SIZE) return false; // Too many battlers
        if (activeBattleUnits.ContainsKey(battler)) return false; // Battler already exists in this zone

        return true;
    }
}
