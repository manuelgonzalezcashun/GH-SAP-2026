using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[Serializable]
public class Type
{
    private Type[] Weaknesses = {};
    private Type[] Resistances = {};

    public virtual Type[] OverrideWeakness()
    {
        return Weaknesses;
    }
    public virtual Type[] OverrideResistance()
    {
        return Resistances;
    }

    public int CalculateDamage(Type atk)
    {
        Resistances = OverrideResistance();
        Weaknesses = OverrideWeakness();
        for (int i = 0; i<Weaknesses.Count(); i++)
        {
            if (Weaknesses[i] == atk)
            {
                return 1;
            }
            if (Resistances[i] == atk)
            {
                
                return -1;
                
            }
        }
        return 0;
    }

    public class Maker
    {
        private Type[] weakness = null;
        private Type[] resistance = null;

        public Maker WithWeak(Type[] weak)
        {
            this.weakness = weak;
            return this;
        }
        public Maker WithRes(Type[] res)
        {
            this.resistance = res;
            return this;
        }
        public Type Make()
        {
            var type = new Type
            {
                Weaknesses = weakness,
                Resistances = resistance
            };
            return type;
        }
    }
}
