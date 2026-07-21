using UnityEngine;

[CreateAssetMenu(fileName = "New Type", menuName = "Battle System/Create new Type")]
public class SO_Type : ScriptableObject
{
    [Header("Type Details")]
    [SerializeField] private Type[] _Weakness;
    [SerializeField] private Type[] _Resistance;

    public Type MakeType()
    {
        return MakeBaseType();
    }

    private Type MakeBaseType()
    {
        return new Type.Maker()
        .WithRes(_Resistance)
        .WithWeak(_Weakness)
        .Make();
    }
}
