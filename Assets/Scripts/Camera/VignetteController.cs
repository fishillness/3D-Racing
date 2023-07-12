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
        private ChromaticAberration aberration;

        private float vignetteIncrease;
        private float vignetteDecrease;

        private void Start()
        {
            processVolume = GetComponent<PostProcessVolume>();
            processVolume.profile.TryGetSettings(out vignette);
            processVolume.profile.TryGetSettings(out aberration);
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
                aberration.active = true;
            }

            if (car.NormaliaLinearVelocity <= normalizeSpeedVignette)
            {
                if (vignette.intensity.value != 0)
                {
                    vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0, vignetteDecrease);
                    vignetteDecrease += Time.deltaTime / vignetteSpeed;
                }
                aberration.active = false;
            }
        }

    }
}

