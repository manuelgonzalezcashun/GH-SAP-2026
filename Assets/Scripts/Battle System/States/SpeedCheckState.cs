using System.Linq;
using UnityEngine;

public class SpeedCheckState : BattleState
{
    public SpeedCheckState(BattleSystem system) : base(system) { }

    public override void EnterState()
    {
        var playerParty = _system.PlayerParty.Battlers;
        var oppParty = _system.OpponentParty.Battlers;

        var battlersSortedBySpeed = playerParty
        .Concat(oppParty)
        .OrderByDescending(b => b.MaxHealth)
        .ToList();

        _system.SetupTurnQueue(battlersSortedBySpeed);

        while (_system.TurnQueue.Count > 0)
        {
            var current = _system.TurnQueue.Dequeue();
            Debug.Log($"{current.Name}");
        }
    }
}
