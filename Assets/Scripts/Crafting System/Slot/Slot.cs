using UnityEngine;
using UnityEngine.EventSystems;



namespace SlotObject
{
    public abstract class Slot : MonoBehaviour, ISlotHandler
    {
        public virtual void OnDrop(PointerEventData eventData)
        {
            var _slotUnit = eventData.pointerDrag;

            if (_slotUnit == null) return;

            _slotUnit.transform.SetParent(transform);
            _slotUnit.transform.position = transform.position;
        }
        public virtual void OnPointerEnter(PointerEventData eventData)
        {

        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}
