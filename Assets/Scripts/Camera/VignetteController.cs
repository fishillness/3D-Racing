using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Racing
{
    public class VignetteController : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField]
        [Range(0.0f, 1.0f)] private float normalizeSpeedVignette;
        [SerializeField] [Range(0f, 1f)] private float maxValueVignette;
        [SerializeField] private float vignetteSpeed;

        private PostProcessVolume processVolume;
        private Vignette vignette;

        private float vignetteIncrease;
        private float vignetteDecrease;

        private void Start()
        {
            processVolume = GetComponent<PostProcessVolume>();
            processVolume.profile.TryGetSettings(out vignette);
        }

        private void Update()
        {
            if (car.NormaliaLinearVelocity >= normalizeSpeedVignette)
            {
                if (vignette.intensity.value != maxValueVignette)
                {
                    vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, maxValueVignette, vignetteIncrease);
                    vignetteIncrease += Time.deltaTime / vignetteSpeed;
                }
            }

            if (car.NormaliaLinearVelocity <= normalizeSpeedVignette)
            {
                if (vignette.intensity.value != 0)
                {
                    vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0, vignetteDecrease);
                    vignetteDecrease += Time.deltaTime / vignetteSpeed;
                }
            }

            if (Input.GetKeyDown(KeyCode.V)) vignetteDecrease = 0;
            if (Input.GetKeyDown(KeyCode.F)) vignetteIncrease = 0;
            
            
            /*
            if (Input.GetKeyDown(KeyCode.B))
                bloom.active = !bloom.active;
            */
        }

    }
}

