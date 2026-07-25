using UnityEngine;

[CreateAssetMenu(fileName = "New Summon", menuName = "Battle System/Move Effect/Create Summon")]

public class SO_Summon : ScriptableObject
{
    [Header("Summon Details")]
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
