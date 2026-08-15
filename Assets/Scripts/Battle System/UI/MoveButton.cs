using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButton : Button
{
    private Move move = null;
    private TMP_Text moveText => GetComponentInChildren<TMP_Text>();

    public void SetupMoveButton(Move move)
    {
        // Setup Move Button Display
        this.move = move;
        moveText.text = move.Name;

        // Add Button Listeners
        onClick.AddListener(() => EventBus.Raise(new MoveSelectedEvent { move = move }));
        onClick.AddListener(() => EventBus.Raise(new ShowOptionsEvent { MO_Show = false, MO_Battler = null }));
        onClick.AddListener(() => EventBus.Raise(new DisplayBattleTextEvent { battleText = string.Empty }));
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        onClick.RemoveAllListeners();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        EventBus.Raise(new DisplayBattleTextEvent { battleText = move.Info });
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        EventBus.Raise(new DisplayBattleTextEvent { battleText = string.Empty });
    }
}
