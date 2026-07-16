using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class TypeManager : MonoBehaviour
{
    private Type[] Weaknesses = {};
    private Type[] Resistances = {};
    private SulfurType sulfurType;

    public virtual Type[] OverrideWeakness()
    {
        return Weaknesses;
    }
    public virtual Type[] OverrideResistance()
    {
        return Resistances;
    }

    public int CalculateDamage(Type atk, int damage)
    {
        Resistances = OverrideResistance();
        Weaknesses = OverrideWeakness();
        for (int i = 0; i<Weaknesses.Count(); i++)
        {
            if (Weaknesses[i] == atk)
            {
                return damage+1;
            }
            if (Resistances[i] == atk)
            {
                if (damage - 1 >= 0)
                {
                    return damage-1;
                }
            }
        }
        return damage;
    }


}
