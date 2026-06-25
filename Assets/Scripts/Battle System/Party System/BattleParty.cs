using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleParty : MonoBehaviour
{
    [SerializeField] int PARTY_SIZE = 4;
    [SerializeField] SO_Battler[] _members;
    private List<Battler> _battlers = new();
    public List<Battler> Battlers => _battlers;
    protected int _battlersCount = 0;
    void OnValidate()
    {
        _members ??= new SO_Battler[PARTY_SIZE];

        if (PARTY_SIZE < 0) return;

        if (_members.Length != PARTY_SIZE)
            Array.Resize(ref _members, PARTY_SIZE);
    }

    void Awake()
    {
        foreach (var member in _members)
        {
            if (member == null) return;
            _battlers.Add(member.CreateBattler());
        }
    }

    protected void AddPartyMember(SO_Battler so_battler)
    {
        if (_battlersCount < PARTY_SIZE)
        {
            _members[_battlersCount] = so_battler;
            _battlersCount++;
        }
    }
    public abstract Battler GetBattler();
}