using UnityEngine;

public class Effect_Rainbow : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool Enabled = true;

    private void Start() => spriteRenderer = GetComponent<SpriteRenderer>();

    private void Update() { if (Enabled) spriteRenderer.color = Color.HSVToRGB(Mathf.PingPong(Time.time * 0.5f, 1), 1, 1); }

    public void Enable() => Enabled = true;
    public void Disable() => Enabled = false;
}
