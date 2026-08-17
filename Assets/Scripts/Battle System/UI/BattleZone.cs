using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleZone : MonoBehaviour
{
    const int ZONE_SIZE = 20;
    Dictionary<Battler, BattleUnit> activeBattleUnits = new Dictionary<Battler, BattleUnit>();
    Queue<BattleUnit> unitPool = new Queue<BattleUnit>();
    [SerializeField] Animator damageAnimator = null;
    [SerializeField] Zone _zone;
    [SerializeField] BattleUnit unitPrefab;
    void OnEnable()
    {
        EventBus.Subscribe<OnZoneSelectedEvent>(ClearBattler);
        EventBus.Subscribe<EndBattleEvent>(ClearBattler);
        EventBus.Subscribe<DamageUnitEvent>(SetupDamageAnimation);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<OnZoneSelectedEvent>(ClearBattler);
        EventBus.Subscribe<EndBattleEvent>(ClearBattler);
        EventBus.UnSubscribe<DamageUnitEvent>(SetupDamageAnimation);
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
        BattleUnit _battleUnit = unitPool.Count > 0
        ? unitPool.Dequeue()
        : Instantiate(unitPrefab, transform);

        _battleUnit.gameObject.SetActive(true);
        return _battleUnit;
    }
    private void ClearBattler(Battler battler)
    {
        if (activeBattleUnits.TryGetValue(battler, out BattleUnit unit))
        {
            ReturnToUnitPool(unit);
            activeBattleUnits.Remove(battler);
        }
    }

    private void ReturnToUnitPool(BattleUnit unit)
    {
        unit.ClearUnit();
        unitPool.Enqueue(unit);
    }
    private void ReturnToUnitPool()
    {
        BattleUnit[] units = transform.GetComponentsInChildren<BattleUnit>();

        foreach (BattleUnit unit in units)
        {
            unit.ClearUnit();
            unitPool.Enqueue(unit);
        }
    }

    void ClearBattler(EndBattleEvent data)
    {
        ReturnToUnitPool();
        activeBattleUnits.Clear();
    }
    private void SetupDamageAnimation(DamageUnitEvent data)
    {
        if (activeBattleUnits.TryGetValue(data.battler, out BattleUnit unit))
        {
            Transform unitTransform = unit.transform;
            damageAnimator.gameObject.transform.position = unitTransform.position;
            StartCoroutine(PlayDamageAnimation());
        }
    }
    private bool CanMoveToThisZone(Battler battler, Zone zone)
    {
        if (_zone != zone) return false; // Ignores Battler if not set to this zone
        if (activeBattleUnits.Count >= ZONE_SIZE) return false; // Too many battlers
        if (activeBattleUnits.ContainsKey(battler)) return false; // Battler already exists in this zone

        return true;
    }

    #region Damage Animation: Slash Attack
    readonly int damageHash = Animator.StringToHash("SliceAttack");
    readonly int idleHash = Animator.StringToHash("Idle");
    IEnumerator PlayDamageAnimation()
    {
        damageAnimator.gameObject.SetActive(true);
        damageAnimator.CrossFade(damageHash, 0, 0);

        yield return new WaitForSeconds(1f);
        damageAnimator.CrossFade(idleHash, 0, 0);
        damageAnimator.gameObject.SetActive(false);
    }
    #endregion
}
