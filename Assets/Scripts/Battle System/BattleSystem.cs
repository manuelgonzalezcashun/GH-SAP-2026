using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BattleSystem : StateMachine
{
    #region Singleton
    public static BattleSystem _instance = null;
    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);

        _instance = this;
    }
    #endregion

    [SerializeField] TrainerParty _playerParty = null;
    [SerializeField] TrainerParty _oppParty = null;
    [SerializeField] RectTransform battleCanvas = null;
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
        EventBus.Subscribe<EndBattleEvent>(EndBattle);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<MoveSelectedEvent>(OnMoveSelected);
        EventBus.UnSubscribe<EndBattleEvent>(EndBattle);
    }

    // TODO: Change Start to Custom Method For Entering Battle

    // void Start()
    // {
    //     SetState(new SetupBattleState(this));
    // }
    void Update()
    {
        if (_currentState == null) return;

        _currentState.UpdateState();
    }
    public void EnterBattle(TrainerParty _playerParty, TrainerParty _oppParty)
    {
        GameManager.Instance.SetState(new InBattleState());
        this._playerParty = _playerParty;
        this._oppParty = _oppParty;

        InputHandler.ChangeActionMaps(InputHandler.combatInput);
        ShowBattleCanvas(true);
        SetState(new SetupBattleState(this));
    }

    public void ShowBattleCanvas(bool show)
    {
        battleCanvas.gameObject.SetActive(show);
    }

    public void OnMoveSelected(MoveSelectedEvent data)
    {
        if (_currentState is BattleState battleState)
            StartCoroutine(battleState.Attack(data.move));
    }
    public void OnPassButton()
    {
        if (_currentState is BattleState battleState)
            StartCoroutine(battleState.Pass());
    }
    public void OnRunButton()
    {
        if (_currentState is BattleState battleState)
            StartCoroutine(battleState.Run());
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
    void EndBattle(EndBattleEvent data)
    {
        _playerParty = null;
        _oppParty = null;
        ActiveBattler = null;

        TurnQueue.Clear();
        AllBattlers.Clear();
        GameManager.Instance.SetState(new InOverworldState());
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
            if (battler.Health <= 0) continue;

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