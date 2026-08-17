using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] Image battlerImage = null;
    private Animator animator => GetComponent<Animator>();
    private Battler _battler;
    public Battler Battler => _battler;
    Color originalColor => Color.white;
    Color selectedColor => Color.red;

    #region Unit Animations
    readonly int takeDamageHash = Animator.StringToHash("TakeDamage");
    readonly int idleHash = Animator.StringToHash("Idle");
    #endregion

    void OnEnable()
    {
        EventBus.Subscribe<SelectTargetEvent>(HighlightBattler);
        EventBus.Subscribe<TargetFaintedEvent>(ClearUnit);
        EventBus.Subscribe<DamageUnitEvent>(DamageUnit);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<SelectTargetEvent>(HighlightBattler);
        EventBus.UnSubscribe<TargetFaintedEvent>(ClearUnit);
        EventBus.UnSubscribe<DamageUnitEvent>(DamageUnit);
    }

    public void SetBattlerInUnit(Battler battler)
    {
        _battler = battler;
        if (_battler == null) return;

        battlerImage.sprite = battler.Sprite;
        battlerImage.color = originalColor;
        animator.enabled = false;
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
        gameObject.SetActive(false);
    }
    void HighlightBattler(SelectTargetEvent data)
    {
        Color highlightColor = (data._Target != null && data._Target == _battler)
         ? selectedColor
         : originalColor;

        battlerImage.color = highlightColor;
    }

    void DamageUnit(DamageUnitEvent data)
    {
        if (data.battler != Battler) return;

        animator.enabled = true;
        StartCoroutine(PlayDamageAnimation());
    }
    IEnumerator PlayDamageAnimation()
    {
        animator.CrossFade(takeDamageHash, 0, 0);
        yield return new WaitForSeconds(1f);
        animator.enabled = false;
        animator.CrossFade(idleHash, 0, 0);
    }
}
