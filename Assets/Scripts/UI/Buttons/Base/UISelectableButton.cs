using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Racing
{
    public class UISelectableButton : UIButton
    {
        public UnityEvent OnSelect;
        public UnityEvent OnUnSelect;

        [SerializeField] private Image selectImage;

        public override void SetFocuse()
        {
            base.SetFocuse();

            selectImage.enabled = true;
            OnSelect?.Invoke();
        }

        public override void SetUnFocuse()
        {
            base.SetUnFocuse();

            selectImage.enabled = false;
            OnUnSelect?.Invoke();
        }
    }
}
