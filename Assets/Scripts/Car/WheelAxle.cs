using System;
using UnityEngine;

namespace Racing
{
    [System.Serializable]
    public class WheelAxle
    {
        #region Properties
        [SerializeField] private WheelCollider leftWheelCollider;
        [SerializeField] private WheelCollider rightWheelCollider;

        [SerializeField] private Transform leftWheelMesh;
        [SerializeField] private Transform rightWheelMesh;

        [SerializeField] private bool isMotor;
        [SerializeField] private bool isSteer;

        [SerializeField] private float wheelWidth;
        [SerializeField] private float antiRollForce;
        [SerializeField] private float additionalWheelDownForce;

        [SerializeField] private float baseForwardStiffnes = 1.5f;
        [SerializeField] private float stabilityForwardFactor = 1.0f;
        [SerializeField] private float baseSidewaysStiffnes = 2.0f;
        [SerializeField] private float stabilitySidewaysFactor = 1.0f;

        private WheelHit leftWheelHit;
        private WheelHit rightWheelHit;
        #endregion

        #region Public API
        public void Update()
        {
            UpdateWheelHits();

            ApplyAntiRoll();
            ApplyDownForce();
            CorrectStiffness();

            SyncMeshTransform();
        }

        public void ApplySteerAngle(float steerAngle, float wheelBaseLenght)
        {
            if (isSteer == false) return;

            float radius = Mathf.Abs(wheelBaseLenght * Mathf.Tan(Mathf.Deg2Rad * ( 90 - Mathf.Abs(steerAngle) )));
            float angleSing = Mathf.Sign(steerAngle);

            if (steerAngle > 0)
            {
                leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / 
                    (radius + (wheelWidth * 0.5f))) * angleSing;
                rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght /
                    (radius - (wheelWidth * 0.5f))) * angleSing;
            }
            else if (steerAngle < 0)
            {
                leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght /
                    (radius - (wheelWidth * 0.5f))) * angleSing;
                rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght /
                    (radius + (wheelWidth * 0.5f))) * angleSing;
            }
            else
            {
                leftWheelCollider.steerAngle = 0;
                rightWheelCollider.steerAngle = 0;
            }
        }

        public void ApplyMotorTorque(float motorTorque)
        {
            if (isMotor == false) return;

            leftWheelCollider.motorTorque = motorTorque;
            rightWheelCollider.motorTorque = motorTorque;
        }
        public void ApplyBrakeTorque(float brakeTorque)
        {
            leftWheelCollider.brakeTorque = brakeTorque;
            rightWheelCollider.brakeTorque = brakeTorque;
        }
        #endregion

        #region Private Methods
        private void UpdateWheelHits()
        {
            leftWheelCollider.GetGroundHit(out leftWheelHit);
            rightWheelCollider.GetGroundHit(out rightWheelHit);
        }

        private void CorrectStiffness()
        {
            WheelFrictionCurve leftForward = leftWheelCollider.forwardFriction;
            WheelFrictionCurve rightForward = rightWheelCollider.forwardFriction;

            WheelFrictionCurve leftSideways = leftWheelCollider.sidewaysFriction;
            WheelFrictionCurve rightSideways = rightWheelCollider.sidewaysFriction;

            leftForward.stiffness = baseForwardStiffnes + Mathf.Abs(leftWheelHit.forwardSlip) *
                stabilityForwardFactor;
            rightForward.stiffness = baseForwardStiffnes + Mathf.Abs(rightWheelHit.forwardSlip) *
                stabilityForwardFactor;

            leftSideways.stiffness = baseSidewaysStiffnes + Mathf.Abs(leftWheelHit.sidewaysSlip) *
                stabilitySidewaysFactor;
            rightForward.stiffness = baseSidewaysStiffnes + Mathf.Abs(rightWheelHit.sidewaysSlip) *
                stabilitySidewaysFactor;

            leftWheelCollider.forwardFriction = leftForward;
            rightWheelCollider.forwardFriction = rightForward;

            leftWheelCollider.sidewaysFriction = leftSideways;
            rightWheelCollider.sidewaysFriction = rightSideways;
        }

        private void ApplyDownForce()
        {
            if (leftWheelCollider.isGrounded == true)
                leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * 
                    -additionalWheelDownForce * leftWheelCollider.attachedRigidbody.velocity.magnitude,
                    leftWheelCollider.transform.position);

            if (rightWheelCollider.isGrounded == true)
                rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal *
                    -additionalWheelDownForce * rightWheelCollider.attachedRigidbody.velocity.magnitude,
                    rightWheelCollider.transform.position);
        }

        private void ApplyAntiRoll()
        {
            float travelL = 1.0f;
            float travelR = 1.0f;

            if (leftWheelCollider.isGrounded == true)
                travelL = (-leftWheelCollider.transform.InverseTransformPoint(leftWheelHit.point).y
                    - leftWheelCollider.radius) / leftWheelCollider.suspensionDistance;

            if (rightWheelCollider.isGrounded == true)
                travelR = (-rightWheelCollider.transform.InverseTransformPoint(rightWheelHit.point).y
                    - rightWheelCollider.radius) / rightWheelCollider.suspensionDistance;

            float forceDir = (travelL - travelR);

            if (leftWheelCollider.isGrounded == true)
                leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelCollider.transform.up *
                    -forceDir * antiRollForce, leftWheelCollider.transform.position);

            if (rightWheelCollider.isGrounded == true)
                rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelCollider.transform.up *
                    forceDir * antiRollForce, rightWheelCollider.transform.position);
        }

        private void SyncMeshTransform()
        {
            UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
            UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
        }

        private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 position;
            Quaternion rotation;
            wheelCollider.GetWorldPose(out position, out rotation);

            wheelTransform.position = position;
            wheelTransform.rotation = rotation;
        }
        #endregion
    }
}

