using UnityEngine;
using UnityEngine.UI;

public class ZoneButton : MonoBehaviour
{
    public Button CurrentZoneBtn => GetComponent<Button>();
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
}
