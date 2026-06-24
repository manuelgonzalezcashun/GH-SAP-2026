using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] Image battlerImage = null;
    private Battler _battler;
    public Battler Battler => _battler;

    public void SetBattlerInUnit(Battler battler)
    {
        _battler = battler;
        battlerImage.color = _battler.Color;
    }
}
