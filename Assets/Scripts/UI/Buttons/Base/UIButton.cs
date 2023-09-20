using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Racing
{
    public class UIButton : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public UnityEvent OnClick;
        public event UnityAction<UIButton> PointerEnter;
        public event UnityAction<UIButton> PointerExit;
        public event UnityAction<UIButton> PointerClick;

        [SerializeField] protected bool Interactable;

        private bool focuse = false;
        public bool IsFocuse => focuse;
   
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerEnter?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerExit?.Invoke(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerClick?.Invoke(this);
            OnClick?.Invoke();
        }

        public virtual void SetFocuse()
        {
            if (Interactable == false) return;

            focuse = true;
        }

        public virtual void SetUnFocuse()
        {
            if (Interactable == false) return;

            focuse = false;
        }

        public virtual void SetInteractable()
        {
            Interactable = true;
        }

        public virtual void SetNonInteractable()
        {
            Interactable = false;
        }

    }
}
