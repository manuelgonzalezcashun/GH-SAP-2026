using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BattleHUD : MonoBehaviour
{
    [SerializeField] RectTransform moveContainer;

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider healthBar;
    [SerializeField] Image fighterImage;

    private Battler _battler;
    public void SetupBattleHUD(Battler battler)
    {
        BattleEvents.onShowOptions += ShowBattleOptions;
        _battler = battler;
        _battler.onHealthChanged += SetHP;

        nameText.text = _battler.Name;
        SetHP(_battler.Health, _battler.MaxHealth);
        fighterImage.color = _battler.Color;
    }
    public void ShowBattleOptions(bool show)
    {
        if (moveContainer == null) return;
        moveContainer.gameObject?.SetActive(show);
    }
    public void SetHP(float health, float maxHealth)
    {
        healthText.text = $"{health} / {maxHealth}";
        healthBar.value = health / maxHealth;
    }
    public void ClearBattleHUD()
    {
        BattleEvents.onShowOptions -= ShowBattleOptions;
        _battler.onHealthChanged -= SetHP;
        _battler = null;
    }
}
