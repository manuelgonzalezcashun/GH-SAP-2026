using UnityEngine;
using UnityEngine.EventSystems;

namespace SlotObject
{
    public abstract class SlotUnit : MonoBehaviour, ISlotUnitHandler
    {
        protected Transform draggingLayer = null;
        protected Transform _initialParent = null;
        private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

        protected virtual void Awake()
        {
            draggingLayer = transform.parent.parent.parent.parent;
        }
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
            _initialParent = transform.parent;

            if (draggingLayer == null) return;
            transform.SetParent(draggingLayer);
        }
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            if (transform.parent == draggingLayer)
            {
                transform.SetParent(_initialParent);
                transform.position = _initialParent.position;
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.Translate(eventData.delta);
        }
    }
}

