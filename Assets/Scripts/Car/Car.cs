using UnityEngine;

namespace Racing 
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float maxBrakeTorque;

        [SerializeField] private AnimationCurve engineTorqueCurve;
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float maxSpeed;

        private CarChassis chassis;

        [Header("DEBUG")]
        public float linearVelocity;
        public float ThrottleControl;
        public float SteerControl;
        public float BrakeControl;
        //public float HandBrakeControl;

        public float MaxSpeed => maxSpeed;
        public float LinearVelocity => chassis.LinearVelocity;
        public float WheelSpeed => chassis.GetWheelSpeed();

        private void Start()
        {
            chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            //DEBUG
            linearVelocity = LinearVelocity;

            float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * maxMotorTorque;
            if (LinearVelocity >= maxSpeed)
                engineTorque = 0;

            chassis.MotorTorque = engineTorque * ThrottleControl;
            chassis.SteerAngle = maxSteerAngle * SteerControl;
            chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
        }
    }
}

