using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Battler", menuName = "Battle System/Create new Battler")]
public class SO_Battler : ScriptableObject
{
    [Header("Battler Stats")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _initiative;
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
        .WithMaxHealth(_maxHealth)
        .WithHealth()
        .WithInitiative(_initiative)
        .WithMoves(moves.Select(move => move.MakeMove()).ToArray())
        .Build();
    }
}
