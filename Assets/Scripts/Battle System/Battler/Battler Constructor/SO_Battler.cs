using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Battler", menuName = "Battle System/Create new Battler")]
public class SO_Battler : ScriptableObject
{
    [Header("Battler Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _initiative;
    [SerializeField] private SO_Move[] moves;
    [SerializeField] private Type type1;
    [SerializeField] private Type type2;

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
        .WithFirstType(type1)
        .WithSecondType(type2)
        .Build();
    }
}
