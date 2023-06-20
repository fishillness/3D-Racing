using System;
using UnityEngine;

namespace Racing
{
    public class CarChassis : MonoBehaviour
    {
        [SerializeField] private WheelAxle[] wheelAxles;
        [SerializeField] private float wheelBaseLength;

        [Header("DEBUG")]
        public float MotorTorque;
        public float BrakeTorque;
        public float SteerAngle;

        /*
        [SerializeField] private float motorTorque;
        [SerializeField] private float brakeTorque;
        [SerializeField] private float steerAngle;
        */

        private void FixedUpdate()
        {
            UpdateWheelAxles();
        }

        private void UpdateWheelAxles()
        {
            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].Update();

                wheelAxles[i].ApplyMotorTorque(MotorTorque);
                wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
                wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
            }
        }
    }
}