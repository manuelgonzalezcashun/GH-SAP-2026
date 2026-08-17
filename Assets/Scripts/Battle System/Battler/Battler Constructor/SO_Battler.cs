using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Battler", menuName = "Battle System/Create new Battler")]
public class SO_Battler : ScriptableObject
{
    [Header("Battler Stats")]
    [SerializeField] private SO_Species _species;
    [SerializeField] private SO_Move[] moves;
    [Header("Battler Display")]
    // [SerializeField] private Color _color = Color.white;
    [SerializeField] private Sprite _sprite = null;

    public Battler CreateBattler()
    {
        return CreateBaseBattler();
    }

    private Battler CreateBaseBattler()
    {
        return new Battler.Builder()
        .WithName(name)
        .WithSpecies(_species.CreateSpecies())
        .WithSprite(_sprite)
        .WithMaxHealth()
        .WithHealth()
        .WithInitiative()
        .WithMoves(moves.Select(move => move.MakeMove()).ToArray())
        .WithFirstType()
        .WithSecondType()
        .Build();
    }
}
