using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class CarSpeedIndicator : MonoBehaviour
    {
        private Car car;
        private Text speedText;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            speedText = GetComponent<Text>();
        }

        private void FixedUpdate()
        {
            speedText.text = Mathf.Round(car.LinearVelocity).ToString().PadLeft(3, '0');
        }
    }
}

