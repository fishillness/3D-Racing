using System;
using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarChassis : MonoBehaviour
    {
        #region Properties
        [SerializeField] private WheelAxle[] wheelAxles;
        [SerializeField] private float wheelBaseLength;
        [SerializeField] private Transform centerOfMass;

        [Header("DownForce")]
        [SerializeField] private float downForceMin;
        [SerializeField] private float downForceMax;
        [SerializeField] private float downForceFactor;

        [Header("AngularDrag")]
        [SerializeField] private float angularDragMin;
        [SerializeField] private float angularDragMax;
        [SerializeField] private float angularDragFactor;

        [Header("DEBUG")]
        public float MotorTorque;
        public float BrakeTorque;
        public float SteerAngle;

        /*
        [SerializeField] private float motorTorque;
        [SerializeField] private float brakeTorque;
        [SerializeField] private float steerAngle;
        */

        private new Rigidbody rigidbody;

        public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;
        #endregion

        #region Unity Events
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (centerOfMass != null)
                rigidbody.centerOfMass = centerOfMass.localPosition;
        }

        private void FixedUpdate()
        {
            UpdateAngularDrag();
            UpdateDownForce();
            UpdateWheelAxles();
        }
        #endregion

        #region Public API
        public float GetAverageRpm()
        {
            float sum = 0;
            for (int i =0; i < wheelAxles.Length; i++)
            {
                sum += wheelAxles[i].GetAvarageRpm();
            }

            return sum / wheelAxles.Length;
        }

        public float GetWheelSpeed()
        {
            return GetAverageRpm() * wheelAxles[0].GetRadius() * 2 * 0.1885f; 
        }

        #endregion

        #region Private methods
        private void UpdateAngularDrag()
        {
            rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity,
                angularDragMin, angularDragMax);
        }

        private void UpdateDownForce()
        {
            float downForce = Mathf.Clamp(downForceFactor * LinearVelocity,
                downForceMin, downForceMax);
            rigidbody.AddForce(-transform.up * downForce);
        }

        private void UpdateWheelAxles()
        {
            int amountMotorWheel = 0;
            for (int i = 0; i < wheelAxles.Length; i++) //Вынести в старт?
            {
                if (wheelAxles[i].IsMotor == true)
                    amountMotorWheel += 2;
            }

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].Update();

                wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
                wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
                wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
            }
        }
        #endregion
    }
}