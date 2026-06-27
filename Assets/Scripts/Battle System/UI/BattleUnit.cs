using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] Image battlerImage = null;
    private Battler _battler;
    public Battler Battler => _battler;
    Color originalColor;
    Color selectedColor = Color.yellow;

    void OnEnable()
    {
        BattleEvents.onSelectTarget += HighlightBattler;
    }
    void OnDisable()
    {
        BattleEvents.onSelectTarget -= HighlightBattler;
    }

    public void SetBattlerInUnit(Battler battler)
    {
        _battler = battler;
        originalColor = _battler.Color;
        battlerImage.color = originalColor;
    }
    public void ClearUnit()
    {
        _battler = null;
        battlerImage.color = Color.white;
        gameObject.SetActive(false);
    }
    void HighlightBattler(Battler target)
    {
        if (target == null)
        {
            battlerImage.CrossFadeColor(originalColor, 0f, true, true);
            return;
        }

        Color hightlightColor = target == _battler ? selectedColor : originalColor;
        battlerImage.CrossFadeColor(hightlightColor, 0.15f, true, true);
    }
}
