using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider healthBar;

    [Header("Highlight Colors")]
    [SerializeField] Color nameTagColorHighlight;
    [SerializeField] Color hudColorHighlight;
    private Battler _battler;
    private Image _battleHUD => GetComponent<Image>();
    private Color hudColor => new(255, 255, 255, 0.4f);
    void OnEnable()
    {
        EventBus.Subscribe<SelectTargetEvent>(HighlightBattleHUD);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<SelectTargetEvent>(HighlightBattleHUD);
    }
    public void SetupBattleHUD(Battler battler)
    {
        _battler = battler;
        if (_battler.Health <= 0) return;

        _battler.onHealthChanged += SetHP;

        string displayName = _battler.Team != Team.PLAYER ? $"{battler.Name} (Enemy)" : battler.Name;
        _battler.SetDisplayName(displayName);
        nameText.text = displayName;

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
    public void ChangeNameColor(bool currentTurn)
    {
        nameText.color = currentTurn
        ? nameTagColorHighlight
        : Color.white;
    }
    public void HighlightBattleHUD(SelectTargetEvent data)
    {
        if (data._Target != _battler)
        {
            _battleHUD.color = hudColor;
            return;
        }

        _battleHUD.color = hudColorHighlight;
    }
}
