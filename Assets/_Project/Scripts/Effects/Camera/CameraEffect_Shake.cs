using UnityEngine;

public class CameraEffect_Shake : MonoBehaviour
{
    [HideInInspector] public static CameraEffect_Shake Instance;

    [Header("Settings")]
    [SerializeField] private float Smoothing = 10;
    [SerializeField] private float Damping = 0.99f;

    [Header("Debug")]
    [SerializeField] private Vector3 OriginalPosition;
    [SerializeField] private Vector3 ShakePosition;
    [Space]
    [SerializeField] private float Intensity;
    [SerializeField] private float Frequency;
    [SerializeField] private float ShakeDuration;
    [SerializeField] private float FrequencyCounter;

    private void Awake() => Instance = this;

    private void Start() => OriginalPosition = transform.localPosition;

    private void Update()
    {
        // Check if can shake
        ShakeDuration -= Time.deltaTime;
        if (ShakeDuration <= 0) return;

        FrequencyCounter -= Time.deltaTime;
        if (FrequencyCounter <= 0)
        {
            FrequencyCounter = Frequency;

            // Generate the shake position
            ShakePosition = OriginalPosition + Random.insideUnitSphere * Intensity;
            ShakePosition.z = OriginalPosition.z;
        }

        // Smoothly move the camera to the shake position
        transform.localPosition = Vector3.Lerp(transform.localPosition, ShakePosition, Smoothing * Time.deltaTime);

        // Apply damping
        ShakePosition *= Damping;
    }

    public void Shake(CameraEffect_Shake_Settings _settings)
    {
        ShakeDuration = _settings.Duration;
        Frequency = _settings.Frequency;
        Intensity = _settings.Intensity;
        FrequencyCounter = 0;
    }
}