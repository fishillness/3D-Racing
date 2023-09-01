using System;
using UnityEngine;

namespace Racing
{
    public class CarInputControl : MonoBehaviour
    {
        private const float directionMeasurementError = 0.5f;

        [SerializeField] private Car car;
        [SerializeField] private AnimationCurve breakCurve;
        [SerializeField] private AnimationCurve steerCurve;

        [SerializeField] [Range(0.0f, 1.0f)] private float autoBreakStrength;

        private float wheelSpeed;
        private float maxSpeed;
        private float verticalAxis;
        private float horizontalAxis;
        //private float handBreakAxis;

        private void Start()
        {
            maxSpeed = car.MaxSpeed;
        }

        private void Update()
        {
            wheelSpeed = car.WheelSpeed;

            UpdateAxis();
            UpdateThrottleAndBreak();
            UpdateSteer();
            UpdateAutoBreak();
            //UpdateBreak();


            /*
            //DEBUG
            if (Input.GetKeyDown(KeyCode.E))
                car.UpGear();
            if (Input.GetKeyDown(KeyCode.Q))
                car.DownGear();
            */
        }

        private void UpdateAxis()
        {
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
            //handBreakAxis = Input.GetAxis("Jump");
        }

        private void UpdateThrottleAndBreak()
        {
            if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed)
                || Mathf.Abs(wheelSpeed) < directionMeasurementError)
            {
                car.ThrottleControl = Mathf.Abs(verticalAxis);
                car.BrakeControl = 0;
            }
            else
            {
                car.ThrottleControl = 0;
                car.BrakeControl = breakCurve.Evaluate(wheelSpeed / maxSpeed);
            }

            //Gear
            if (verticalAxis < 0 
                && wheelSpeed > -directionMeasurementError 
                && wheelSpeed <= directionMeasurementError)
            {
                car.ShiftToReverseGear();
            }
            if (verticalAxis > 0 
                && wheelSpeed > -directionMeasurementError 
                && wheelSpeed <= directionMeasurementError)
            {
                car.ShiftToFirstGear();
            }
        }

        private void UpdateSteer()
        {
            car.SteerControl = steerCurve.Evaluate(wheelSpeed / maxSpeed) * horizontalAxis;
        }

        private void UpdateAutoBreak()
        {
            if (verticalAxis == 0) 
            {
                car.BrakeControl = breakCurve.Evaluate(wheelSpeed / maxSpeed) * autoBreakStrength;
            }
        }

        public void Stop()
        {
            Reset();

            car.BrakeControl = 1;
        }

        public void Reset()
        {
            verticalAxis = 0;
            horizontalAxis = 0;
            //handBreakAxis = 0;

            car.ThrottleControl = 0;
            car.SteerControl = 0;
            car.BrakeControl = 0;
        }
    }
}
