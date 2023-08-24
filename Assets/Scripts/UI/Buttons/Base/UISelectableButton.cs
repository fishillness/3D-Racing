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

            Debug.Log($"SET selectImage = {selectImage}. active? {selectImage.IsActive()}");
            selectImage.enabled = true;
            Debug.Log($"SET selectImage = {selectImage}. active? {selectImage.IsActive()}");
            OnSelect?.Invoke();
        }

        public override void SetUnFocuse()
        {
            base.SetUnFocuse();


            Debug.Log($"UNSET selectImage = {selectImage}. active? {selectImage.IsActive()}");
            selectImage.enabled = false;
            Debug.Log($"UNSET selectImage = {selectImage}. active? {selectImage.IsActive()}");
            OnUnSelect?.Invoke();
        }
    }
}
