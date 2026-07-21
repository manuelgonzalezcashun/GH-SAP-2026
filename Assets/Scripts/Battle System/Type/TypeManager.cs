using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TypeManager : MonoBehaviour
{
    public int CalculateDamage(Type def, Type atk, int damage)
    {
        if (def == Type.NONE)
        {
            return damage;
        }
        else if (def == Type.SALT)
        {
            if (atk == Type.SULFUR)
            {
                return Weakness(damage);
            }
            else if (atk == Type.MERCURY || atk == Type.SALT)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.SULFUR)
        {
            if (atk == Type.MERCURY)
            {
                return Weakness(damage);
            }
            else if (atk == Type.SALT || atk == Type.SULFUR)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.MERCURY)
        {
            if (atk == Type.SALT || atk == Type.MERCURY)
            {
                return Weakness(damage);
            }
            else if (atk == Type.SULFUR)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.LEAD)
        {
            if (atk == Type.MERCURY)
            {
                return Resistance(damage);
            }
        }

        return damage;
    }

    public int Weakness(int dam)
    {
        return dam+10;
    }
    public int Resistance(int dam)
    {
        if (dam - 10 >= 0)
        {
            return dam-10;
        }
        else
        {
            return 0;
        }
    }


}
