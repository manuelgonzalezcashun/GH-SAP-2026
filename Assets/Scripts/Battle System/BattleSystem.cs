using System;
using System.Collections.Generic;
using System.Linq;
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

    public bool MovePhaseComplete { get; private set; }
    public bool AttackPhaseComplete { get; private set; }

    void OnValidate()
    {
        _zones = (Zone[])Enum.GetValues(typeof(Zone));
    }
    void OnEnable()
    {
        EventBus.Subscribe<MoveSelectedEvent>(OnMoveSelected);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<MoveSelectedEvent>(OnMoveSelected);
    }

    // TODO: Change Start to Custom Method For Entering Battle
    void Start()
    {
        SetState(new SetupBattleState(this));
    }
    void Update()
    {
        _currentState.UpdateState();
    }
    public void OnMoveSelected(MoveSelectedEvent data)
    {
        StartCoroutine(_currentState.Attack(data.move));
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
        InitializeBattlers(_playerParty.Battlers, Team.PLAYER, Zone.P_BACK);
        InitializeBattlers(_oppParty.Battlers, Team.OPPONENT, Zone.O_BACK);

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
    void InitializeBattlers(IEnumerable<Battler> battlers, Team faction, Zone zone)
    {
        foreach (var battler in battlers)
        {
            battler.SetTeam(faction);



            EventBus.Raise(new SetupBattleEvent { _Battler = battler, _Zone = zone });
        }
    }
    public void SetActiveBattler(Battler battler)
    {
        ActiveBattler = battler;
    }
    public Battler GetActiveBattler()
    {
        return ActiveBattler;
    }

    public void UpdateAttackPhaseFlag(bool flag)
    {
        AttackPhaseComplete = flag;
    }
    public void UpdateMovePhaseFlag(bool flag)
    {
        MovePhaseComplete = flag;
    }
    public void ShowMoveOptions()
    {
        EventBus.Raise(new ShowOptionsEvent { MO_Show = true, MO_Battler = ActiveBattler });
    }
}