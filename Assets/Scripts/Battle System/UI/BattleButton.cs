using UnityEngine;
using UnityEngine.UI;

public class BattleButton : Button
{
    [SerializeField] string description;
    public string Description => description;

    protected override void OnEnable()
    {
        onClick.AddListener(() => EventBus.Raise(new DisplayBattleTextEvent { battleText = string.Empty }));
    }
    protected override void OnDisable()
    {
        onClick.RemoveAllListeners();
    }
}
