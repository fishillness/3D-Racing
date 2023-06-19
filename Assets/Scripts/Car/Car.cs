using UnityEngine;

namespace Racing 
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] wheelColliders;
        [SerializeField] private Transform[] wheelMeshs;
        [SerializeField] private float motorTorque;
        [SerializeField] private float brakeTorque;
        [SerializeField] private float steerAngle;

        private void Update()
        {
            for(int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].motorTorque = Input.GetAxis("Vertical") * motorTorque;
                wheelColliders[i].brakeTorque = Input.GetAxis("Jump") * brakeTorque;

                Vector3 position;
                Quaternion rotation;
                wheelColliders[i].GetWorldPose(out position, out rotation);

                wheelMeshs[i].position = position;
                wheelMeshs[i].rotation = rotation;
            }

            wheelColliders[0].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
            wheelColliders[1].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
        }
    }
}

