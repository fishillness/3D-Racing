using UnityEngine;

namespace Racing
{
    public class CarInputControl : MonoBehaviour
    {
        [SerializeField] private Car car;

        private void Update()
        {
            car.ThrottleControl = Input.GetAxis("Vertical");
            car.SteerControl = Input.GetAxis("Horizontal");
            car.BrakeControl = Input.GetAxis("Jump");
        }
    }
}
