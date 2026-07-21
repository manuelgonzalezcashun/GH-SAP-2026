using UnityEngine;
using System;

[Serializable]
public class Mercury : Type
{
    
}

public class Sulfur : Type
{
    public override Type[] OverrideWeakness()
    {
        Type[] temp = new Type[] {new Mercury()};
        return temp;
    }

    public override Type[] OverrideResistance()
    {
        Type[] temp = new Type[] {new Salt()};
        return temp;
    }
    
}

public class Salt : Type
{
    
}

public class Lead : Type
{
    
}
public class Gold : Type
{
    
}
public class None : Type
{
    
}