using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class BattleSystem : StateMachine
{
    [SerializeField] TrainerParty _playerParty = null;
    [SerializeField] TrainerParty _oppParty = null;
    Zone[] _zones;

    public Queue<Battler> TurnQueue { get; private set; } = new Queue<Battler>();
    public List<Battler> AllBattlers { get; private set; }
    public Battler ActiveBattler { get; private set; }
    public Zone[] Zones => _zones;

    void OnValidate()
    {
        _zones = (Zone[])Enum.GetValues(typeof(Zone));
    }

    // TODO: Change Start to Custom Method For Entering Battle
    void Start()
    {
        SetState(new SetupBattleState(this));
    }
    public void OnAttackButton()
    {
        StartCoroutine(_currentState.Attack());
    }
    public void OnHealButton()
    {
        StartCoroutine(_currentState.Heal());
    }
    public void OnRunButton()
    {
        Debug.Log("Player Selected to Run Away!");
    }
    public void SetupBattle()
    {
        InitializeBattlers(_playerParty.Battlers, Faction.PLAYER, Zone.P_BACK);
        InitializeBattlers(_oppParty.Battlers, Faction.OPPONENT, Zone.O_BACK);

        AllBattlers = _playerParty.Battlers
        .Concat(_oppParty.Battlers)
        .OrderByDescending(battler => battler.Initiative)
        .ToList();
    }
    public void SetupTurnQueue()
    {
        TurnQueue.Clear();
        foreach (var battler in AllBattlers) TurnQueue.Enqueue(battler);
    }
    public bool isSideFainted(Faction faction)
    {
        return AllBattlers
        .Where(battler => battler.Faction == faction)
        .All(battler => battler.Health <= 0);
    }
    void InitializeBattlers(IEnumerable<Battler> battlers, Faction faction, Zone zone)
    {
        foreach (var battler in battlers)
        {
            battler.SetFaction(faction);
            BattleEvents.SetBattlerInZone(battler, zone);
            BattleEvents.SetupBattle(battler);
        }
    }
    public void SetActiveBattler(Battler battler)
    {
        ActiveBattler = battler;
    }
}