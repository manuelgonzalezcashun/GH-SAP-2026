using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Move", menuName = "Battle System/Create new Move")]
public class SO_Move : ScriptableObject
{
    [Header("Move Details")]
    [SerializeField] private int _Damage;
    [SerializeField] private int _Healing;
    [Range(-1, 2)][SerializeField] private int _Distance;
    [SerializeField] private bool _HitsAllInRow;
    [SerializeField] private bool _CanTargetAlly;
    [SerializeField] private Type _Type;
    [SerializeField] private string _Description;
    [SerializeField] private MoveCategory _Category;
    [SerializeField] private SO_Effect[] _Effect;

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
        .WithHealing(_Healing)
        .WithDistance(_Distance)
        .WithRow(_HitsAllInRow)
        .WithAllyHit(_CanTargetAlly)
        .WithCategory(_Category)
        //.WithEffect(_Effect.Select(effect => effect.MakeEffect()).ToArray())
        .Make();
    }
}
