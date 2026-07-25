using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider healthBar;

    private Battler _battler;
    void OnEnable()
    {
        EventBus.Subscribe<TargetFaintedEvent>(ClearBattleHUD);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<TargetFaintedEvent>(ClearBattleHUD);
    }
    public void SetupBattleHUD(Battler battler)
    {
        _battler = battler;
        _battler.onHealthChanged += SetHP;

        nameText.text = _battler.Name;
        SetHP(_battler.Health, _battler.MaxHealth);
    }
    public void SetHP(float health, float maxHealth)
    {
        healthText.text = $"{health} / {maxHealth}";
        healthBar.value = health / maxHealth;
    }
    public void ClearBattleHUD(TargetFaintedEvent data)
    {
        if (data._Target != _battler) return;
        ClearBattleHUD();
    }
    public void ClearBattleHUD()
    {
        gameObject.SetActive(false);
        _battler.onHealthChanged -= SetHP;
        _battler = null;
    }
}
