using UnityEngine;

public class ZoneButton : MonoBehaviour
{
    [SerializeField] Zone destinationZone;

    private Battler _activeBattler = null;

    void OnEnable()
    {
        BattleEvents.onBroadcastActiveBattler += GetActiveBattler;
    }
    void OnDisable()
    {
        BattleEvents.onBroadcastActiveBattler -= GetActiveBattler;
    }
    public void MoveBattler()
    {
        if (_activeBattler == null) return;
        BattleEvents.SetBattlerInZone(_activeBattler, destinationZone);
    }
    void GetActiveBattler(Battler battler)
    {
        _activeBattler = battler;
    }
}
