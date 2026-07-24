using UnityEngine;

[CreateAssetMenu(fileName = "New EX_DMG", menuName = "Battle System/Move Effect/Create Extra Damage")]


public class SO_EX_DMG : ScriptableObject
{
    [Header("Damage Details")]
    [SerializeField] private BonusDamageCalc _Calc;
    

    public EX_DMG MakeDMG()
    {
        return MakeBaseDMG();
    }

    private EX_DMG MakeBaseDMG()
    {
        return new EX_DMG.ExtraMaker()
        .WithCalc(_Calc)
        .Make();
    }
}
