using UnityEngine;

[CreateAssetMenu(fileName = "New Battler", menuName = "Battle System/Create new Battler")]
public class SO_Battler : ScriptableObject
{
    [Header("Battler Stats")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private int _power;
    [SerializeField] private int _healing;

    [Header("Battler Display")]
    [SerializeField] private string _displayName;
    [SerializeField] private Color _color = Color.white;

    public Battler CreateBattler()
    {
        return CreateBaseBattler();
    }

    private Battler CreateBaseBattler()
    {
        return new Battler.Builder()
        .WithName(_displayName)
        .WithColor(_color)
        .WithPower(_power)
        .WithHealing(_healing)
        .WithMaxHealth(_maxHealth)
        .WithHealth()
        .Build();
    }
}
