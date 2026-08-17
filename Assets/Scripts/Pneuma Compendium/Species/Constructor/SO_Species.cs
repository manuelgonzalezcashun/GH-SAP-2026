using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "New Species", menuName = "Battle System/Create new Species")]

public class SO_Species : ScriptableObject
{
    [Header("Species Info")]
    [SerializeField] private string _pneuma;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _initiative;
    [SerializeField] private int _aptitude;
    [SerializeField] private Type type1;
    [SerializeField] private Type type2;
    [SerializeField] private SO_Move[] _one; // Level One Moveset
    [SerializeField] private SO_Move[] _two; // Level Two Moveset
    [SerializeField] private SO_Move[] _three; // level Three Moveset
    [SerializeField] private SO_Move[] _four; // Level Four Moveset
    [SerializeField] private SO_Move[] _five; // Level Five Moveset


    public Species CreateSpecies()
    {
        return CreateBaseSpecies();
    }

    private Species CreateBaseSpecies()
    {
        return new Species.Builder()
        .WithPneuma(_pneuma)
        .WithMaxHealth(_maxHealth)
        .WithInitiative(_initiative)
        .WithAptitude(_aptitude)
        .WithFirstType(type1)
        .WithSecondType(type2)
        .WithOne(_one.Select(move => move.MakeMove()).ToArray())
        .WithTwo(_two.Select(move => move.MakeMove()).ToArray())
        .WithThree(_three.Select(move => move.MakeMove()).ToArray())
        .WithFour(_four.Select(move => move.MakeMove()).ToArray())
        .WithFive(_five.Select(move => move.MakeMove()).ToArray())

        .Build();
    }
}
