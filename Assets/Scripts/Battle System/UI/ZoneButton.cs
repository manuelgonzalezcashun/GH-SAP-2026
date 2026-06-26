using UnityEngine;

public class ZoneButton : MonoBehaviour
{
    [SerializeField] Zone destinationZone;
    private Battler _activeBattler = null;
    public void MoveBattler()
    {
        if (_activeBattler == null) return;

        BattleEvents.SetBattlerInZone(_activeBattler, destinationZone);
        BattleEvents.MoveComplete();

        _activeBattler = null;
        transform.parent.gameObject.SetActive(false);
    }
    public void SetActiveBattler(Battler battler)
    {
        _activeBattler = battler;
    }
}
