using System;
using UnityEngine;

public enum Zone { P_BACK = 0, P_FRONT = 1, O_FRONT = 2, O_BACK = 3 }
public class BattleArea : MonoBehaviour
{
    [SerializeField] BattleZone[] battleZones;

    void OnEnable()
    {
        EventBus.Subscribe<OnMoveZoneEvent>(SetBattlerInZone);
        EventBus.Subscribe<SetupBattleEvent>(SetBattlerInZone);

    }
    void OnDisable()
    {
        EventBus.UnSubscribe<OnMoveZoneEvent>(SetBattlerInZone);
        EventBus.UnSubscribe<SetupBattleEvent>(SetBattlerInZone);
    }
    private void SetBattlerInZone(OnMoveZoneEvent data)
    {
        if (data._ZoneStep == 0) return;

        int currentZoneIndex = data._Battler.getRow();
        int targetZoneIndex = currentZoneIndex + data._ZoneStep;
        targetZoneIndex = Mathf.Clamp(targetZoneIndex, 0, battleZones.Length - 1);
        Zone destinationZone = (Zone)targetZoneIndex;
        EventBus.Raise(new OnZoneSelectedEvent { _Battler = data._Battler });

        battleZones[targetZoneIndex].SetBattlerInZone(data._Battler, destinationZone);
    }

    void SetBattlerInZone(SetupBattleEvent data)
    {
        battleZones[(int)data._Zone].SetBattlerInZone(data._Battler, data._Zone);
    }

}
