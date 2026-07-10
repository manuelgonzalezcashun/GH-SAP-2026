using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Battler", menuName = "Battle System/Create new Battler")]
public class SO_Battler : ScriptableObject
{
    [Header("Battler Stats")]
    [SerializeField] private float _maxHealth;
    // [SerializeField] private int _power;
    [SerializeField] private float _initiative;
    // [SerializeField] private int _healing;
    [SerializeField] private SO_Move[] moves;

    [Header("Battler Display")]
    [SerializeField] private Color _color = Color.white;

    public Battler CreateBattler()
    {
        return CreateBaseBattler();
    }

    private Battler CreateBaseBattler()
    {
        return new Battler.Builder()
        .WithName(name)
        .WithColor(_color)
        // .WithPower(_power)
        // .WithHealing(_healing)
        .WithMaxHealth(_maxHealth)
        .WithHealth()
        .WithInitiative(_initiative)
        .WithMoves(moves.Select(move => move.MakeMove()).ToArray())
        .Build();
    }
}
