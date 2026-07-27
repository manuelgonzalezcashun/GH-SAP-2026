using UnityEngine;

public abstract class BonusDamageCalc : ScriptableObject
{
    public abstract int bonusDamage(int damage,Battler affected);
    
}
//unsure how to get it to recognize current attacker

public class LowHpFish : BonusDamageCalc
{
    public override int bonusDamage(int damage,Battler affected)
    {
        int additionalDMG = (int) (affected.MaxHealth-affected.Health)/3;
        return damage+additionalDMG;
    }
}

public class HighHpFish : BonusDamageCalc
{
    public override int bonusDamage(int damage,Battler affected)
    {
        int additionalDMG = (int) (affected.MaxHealth-affected.Health)/3;
        return damage-additionalDMG;
    }
}

public class HalveHP : BonusDamageCalc
{
    public override int bonusDamage(int damage,Battler affected)
    {
        int additionalDMG = (int) affected.Health/2;
        return damage-additionalDMG;
    }
}
public class test : BonusDamageCalc
{
    public override int bonusDamage(int damage,Battler affected)
    {
        return damage+10;
    }
}