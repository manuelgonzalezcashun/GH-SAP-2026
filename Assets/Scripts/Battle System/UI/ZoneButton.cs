using UnityEngine;

public class ZoneButton : MonoBehaviour
{
    [SerializeField] Zone destinationZone;
    private Battler _activeBattler = null;
    public void MoveBattler()
    {
        if (_activeBattler == null) return;
        EventBus.Raise(new OnMoveEvent { _Battler = _activeBattler, _Zone = destinationZone });
        _activeBattler = null;
        transform.parent.gameObject.SetActive(false);
    }
    public void SetActiveBattler(Battler battler)
    {
        _activeBattler = battler;
    }
}
