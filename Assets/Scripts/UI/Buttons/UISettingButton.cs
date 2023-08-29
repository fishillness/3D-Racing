using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UISettingButton : UISelectableButton
    {
        [SerializeField] private Setting setting;
        [SerializeField] private Text titleText;
        [SerializeField] private Text valueText;
        [SerializeField] private Image previousImage;
        [SerializeField] private Image nextImage;

        public void SetNextValueSetting() => setting.SetNextValue();
        public void SetPrevoiusValueSetting() => setting.SetPreviousValue();

       public void ApplyProperty(Setting property)
        {
            if (property == null) return;

            setting = property;
            titleText.text = setting.Title;
            valueText.text = setting.GetStringValue();

            previousImage.enabled = !setting.IsMinValue;
            nextImage.enabled = !setting.IsMaxValue;
        }

    }
}
