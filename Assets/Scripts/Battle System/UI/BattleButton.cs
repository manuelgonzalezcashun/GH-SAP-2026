using UnityEngine;
using UnityEngine.EventSystems;
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
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        EventBus.Raise(new DisplayBattleTextEvent { battleText = Description });
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        EventBus.Raise(new DisplayBattleTextEvent { battleText = string.Empty });
    }
}
