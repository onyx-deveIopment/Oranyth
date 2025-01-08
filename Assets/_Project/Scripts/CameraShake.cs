using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [HideInInspector] public static CameraShake Instance;

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

    public void Shake(float _duration, float _frequency, float _intensity)
    {
        ShakeDuration = _duration;
        Frequency = _frequency;
        Intensity = _intensity;
        FrequencyCounter = 0;
    }
}