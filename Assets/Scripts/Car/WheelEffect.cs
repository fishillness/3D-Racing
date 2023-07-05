using UnityEngine;

namespace Racing
{
    public class WheelEffect : MonoBehaviour
    {
        [SerializeField] private GameObject skidPrefab;
        [SerializeField] private WheelCollider[] wheels;
        [SerializeField] private ParticleSystem[] wheelsSmoke;

        [SerializeField] private float forwardSlipLimit;
        [SerializeField] private float sidewaySlipLimit;

        private WheelHit wheelHit;
        private Transform[] skidTrail;

        private void Start()
        {
            skidTrail = new Transform[wheels.Length];
        }

        private void Update()
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetGroundHit(out wheelHit);

                if (wheels[i].isGrounded == true)
                {
                    if (wheelHit.forwardSlip > forwardSlipLimit ||
                        wheelHit.sidewaysSlip > sidewaySlipLimit)
                    {
                        if (skidTrail[i] == null)
                            skidTrail[i] = Instantiate(skidPrefab).transform;

                        if (skidTrail[i] != null)
                        {
                            skidTrail[i].position = wheels[i].transform.position -
                                wheelHit.normal * wheels[i].radius;
                            skidTrail[i].forward = -wheelHit.normal;

                            wheelsSmoke[i].transform.position = skidTrail[i].position;
                            wheelsSmoke[i].Emit(1);
                        }

                        continue;
                    }
                }

                skidTrail[i] = null;
                wheelsSmoke[i].Stop();
            }
        }
    }
}
