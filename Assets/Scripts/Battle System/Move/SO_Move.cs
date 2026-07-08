using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Battle System/Create new Move")]
public class SO_Move : ScriptableObject
{
    [Header("Move Details")]
    [SerializeField] private int _Damage;
    [SerializeField] private int _Distance;
    [SerializeField] private bool _HitsAllInRow;
    [SerializeField] private bool _CanTargetAlly;
    [SerializeField] private string _Type;
    [SerializeField] private string _Description;




    public Move MakeMove()
    {
        return MakeBaseMove();
    }

    private Move MakeBaseMove()
    {
        return new Move.Maker()
        .WithName(name)
        .WithDesc(_Description)
        .WithType(_Type)
        .WithDamage(_Damage)
        .WithDistance(_Distance)
        .WithRow(_HitsAllInRow)
        .WithAllyHit(_CanTargetAlly)
        .Make();
    }
}
