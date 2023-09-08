using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Racing
{
    public class UISeasonButton : UISelectableButton, IScriptableObjectProperty
    {
        [SerializeField] Season season;
        //[SerializeField] private Image icon;
        [SerializeField] private Text title;

        private void Start()
        {
            ApplyProperty(season);
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;

            if (property is Season == false)
                return;

            season = property as Season;
            //icon.sprite = season.Icon;
            title.text = season.SeasonName;
        }
    }
}

