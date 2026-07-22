using UnityEngine;

[CreateAssetMenu(fileName = "New EX_DMG", menuName = "Battle System/Move Effect/Create Extra Damage")]


public class SO_EX_DMG : ScriptableObject
{
    [Header("Damage Details")]
    [SerializeField] private SO_Battler _Template;
    

    public Summon MakeSummon()
    {
        _Template.CreateBattler();
        return MakeBaseSummon();
    }

    private Summon MakeBaseSummon()
    {
        return new Summon.SummonMaker()
        .WithTemplate(_Template)
        .Make();
    }
}
