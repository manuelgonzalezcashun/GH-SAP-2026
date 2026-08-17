using UnityEngine;
using UnityEngine.UI;

public class ZoneButton : Button
{
    [SerializeField] string description;
    public string Description => description;
    private Battler _activeBattler = null;
    public void MoveBattler(int zoneStep)
    {
        if (_activeBattler == null) return;
        EventBus.Raise(new OnMoveZoneEvent { _Battler = _activeBattler, _ZoneStep = zoneStep });
        _activeBattler = null;
        transform.parent.gameObject.SetActive(false);
    }
    public void SetActiveBattler(Battler battler)
    {
        _activeBattler = battler;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(() => EventBus.Raise(new DisplayBattleTextEvent { battleText = string.Empty }));
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        onClick.RemoveAllListeners();
    }
}
