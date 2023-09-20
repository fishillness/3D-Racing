using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class CarEngineIndicator : MonoBehaviour
    {
        private Car car;
        private Text gearboxText;
        private Image engineProgressBar;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            gearboxText = GetComponentInChildren<Text>();
            engineProgressBar = GetComponentInChildren<Image>();

            engineProgressBar.fillAmount = 0f;
        }

        private void FixedUpdate()
        {
            gearboxText.text = (car.SelectedGearIndex + 1).ToString();
            engineProgressBar.fillAmount = car.EngineRpm / car.UpShiftEngineRpm;
        }
    }
}
