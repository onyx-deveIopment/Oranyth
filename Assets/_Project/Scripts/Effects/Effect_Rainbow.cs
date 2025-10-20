using Unity.VisualScripting;
using UnityEngine;

public class Effect_Rainbow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Material EffectMaterial;
    [SerializeField] private Color EffectColor;

    [Header("Settings")]
    [SerializeField] private float LGM_Speed = 1;

    [Header("Debug")]
    [SerializeField] private Material DefaultMaterial;
    [SerializeField] private Color DefaultColor;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private bool Enabled = false;

    private void Start() => Setup();

    private void Setup()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        DefaultMaterial = SpriteRenderer.material;
        DefaultColor = SpriteRenderer.color;
    }

    private void Update()
    {
        switch (GraphicsMode.Instance.GetGM() == GraphicsMode.GraphicMode.Low)
        {
            case true:EffectLowQuality(); break;
            case false: EffectHighQuality(); break;
        }
    }

    private void EffectLowQuality()
    {
        SpriteRenderer.material = DefaultMaterial;
        SpriteRenderer.color = DefaultColor;
            
        if (!Enabled) return;
        SpriteRenderer.color = Color.HSVToRGB(Mathf.PingPong(Time.time * LGM_Speed, 1), 1, 1);
    }
    
    private void EffectHighQuality()
    {
        if (Enabled)
        {
            SpriteRenderer.material = EffectMaterial;
            SpriteRenderer.color = EffectColor;
        }
        else
        {
            SpriteRenderer.material = DefaultMaterial;
            SpriteRenderer.color = DefaultColor;
        }
    }

    public void Enable() => Enabled = true;
    public void Disable() => Enabled = false;
}
