using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleSystem : StateMachine
{
    [SerializeField] TrainerParty _playerParty = null;
    [SerializeField] TrainerParty _oppParty = null;
    public TrainerParty PlayerParty => _playerParty;
    public TrainerParty OpponentParty => _oppParty;

    public Battler Player { get; private set; }
    public Battler Opponent { get; private set; }

    public Queue<Battler> TurnQueue { get; private set; }

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
        Debug.Log($"{Player.Name} ran away...");

    }
    public void OnMoveButton()
    {

    }
    public void SetupBattle(Battler player, Battler opponent)
    {
        Player = player;
        Opponent = opponent;

        BattleEvents.StartBattle(Player, Opponent);
    }
    public void SetupTurnQueue(List<Battler> battlers)
    {
        TurnQueue = new Queue<Battler>(battlers);
    }
}