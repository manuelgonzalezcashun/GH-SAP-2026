using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider healthBar;
    private Battler _battler;
    public void SetupBattleHUD(Battler battler)
    {
        _battler = battler;
        _battler.onHealthChanged += SetHP;

        nameText.text = _battler.Name;
        SetHP(_battler.Health, _battler.MaxHealth);
        gameObject.SetActive(true);
    }
    public void SetHP(float health, float maxHealth)
    {
        healthText.text = $"{health} / {maxHealth}";
        healthBar.value = health / maxHealth;
    }
    public void ClearBattleHUD()
    {
        _battler.onHealthChanged -= SetHP;
        _battler = null;

        nameText.text = string.Empty;
        healthText.text = string.Empty;
        healthBar.value = healthBar.maxValue;

        gameObject.SetActive(false);
    }
}
