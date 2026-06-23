using System;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    private Battler _battlerInstance;
    public Battler GetBattlerInstance => _battlerInstance;
    public void SendIntoBattle(Battler battler)
    {
        if (battler == null) return;
        _battlerInstance = battler;
    }
}
