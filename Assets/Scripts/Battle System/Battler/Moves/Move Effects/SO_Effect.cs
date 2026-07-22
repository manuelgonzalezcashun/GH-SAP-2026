using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Battle System/Move Effect/Create Move Effect")]


public class SO_Effect : ScriptableObject
{
    [Header("Effect Details")]
    [SerializeField] private int _RowsMoved;
    [SerializeField] private SO_Summon _Summon;
    [SerializeField] private SO_EX_DMG _ExtraDamage;


    public Effect MakeEffect()
    {
        return MakeBaseEffect();
    }

    private Effect MakeBaseEffect()
    {
        return new Effect.EffectMaker()
        .WithRowMove(_RowsMoved)
        .WithSummon(_Summon)
        .WithDMG(_ExtraDamage)
        .Make();
    }
}
