using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SulfurType : TypeManager
{
    public override Type[] OverrideWeakness()
    {
        Type[] temp = new Type[] {Type.MERCURY};
        return temp;
    }

    public override Type[] OverrideResistance()
    {
        Type[] temp = new Type[] {Type.SALT};
        return temp;
    }
    

}
