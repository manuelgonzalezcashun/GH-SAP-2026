using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] Image battlerImage = null;
    private Battler _battler;
    public Battler Battler => _battler;
    Color originalColor => Color.white;
    Color selectedColor => Color.yellow;

    void OnEnable()
    {
        EventBus.Subscribe<SelectTargetEvent>(HighlightBattler);
        EventBus.Subscribe<TargetFaintedEvent>(ClearUnit);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<SelectTargetEvent>(HighlightBattler);
        EventBus.UnSubscribe<TargetFaintedEvent>(ClearUnit);
    }

    public void SetBattlerInUnit(Battler battler)
    {
        _battler = battler;
        if (_battler == null) return;

        battlerImage.sprite = battler.Sprite;
        battlerImage.color = originalColor;
    }
    public void ClearUnit(TargetFaintedEvent data)
    {
        if (data._Target != _battler) return;
        ClearUnit();
    }
    public void ClearUnit()
    {
        _battler = null;
        battlerImage.sprite = null;
        battlerImage.color = Color.hotPink;
        gameObject.SetActive(false);
    }
    void HighlightBattler(SelectTargetEvent data)
    {
        Color highlightColor = (data._Target != null && data._Target == _battler)
         ? selectedColor
         : originalColor;

        battlerImage.color = highlightColor;
    }
}
