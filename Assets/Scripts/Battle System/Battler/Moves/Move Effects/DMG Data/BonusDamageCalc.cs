using UnityEngine;

public abstract class BonusDamageCalc : ScriptableObject
{
    public abstract int bonusDamage(int damage);
    
}
//unsure how to get it to recognize current attacker

// public class LowHpFish : BonusDamageCalc
// {
//     public override int bonusDamage(int damage)
//     {
//         var attacker = _system.ActiveBattler;
//         int additionalDMG = (attacker.MaxHealth-attacker.Health)/3;
//         return damage+additionalDMG;
//     }
// }

// public class HighHpFish : BonusDamageCalc
// {
//     public override int bonusDamage(int damage)
//     {
//         var attacker = _system.ActiveBattler;
//         int additionalDMG = (attacker.MaxHealth-attacker.Health)/3;
//         return damage-additionalDMG;
//     }
// }

// public class HalveHP : BonusDamageCalc
// {
//     public override int bonusDamage(int damage)
//     {
//         var attacker = _system.ActiveBattler;
//         int additionalDMG = attacker.Health/2;
//         return damage-additionalDMG;
//     }
// }
public class test : BonusDamageCalc
{
    public override int bonusDamage(int damage)
    {
        return damage+10;
    }
}