using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    [HideInInspector] public static GraphController Instance;

    [Header("References")]
    [SerializeField] private Image GraphImage;

    [Header("Settings")]
    [SerializeField] private float[] GraphValues = { 10, 20, 30, 40, 50, 50, 50, 50, 50, 60, 70, 60, 60, 75, 80, 90, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 };
    [SerializeField] private float XScale = 500;
    [SerializeField] private float YScale = 300;

    [Header("Debug")]
    [SerializeField] private Sprite GraphSprite;

    private Texture2D graphTexture;
    private const int textureWidth = 512;
    private const int textureHeight = 300;

    private void Awake() => Instance = this;

    private void Start()
    {
        // Create a new texture
        graphTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        // Fill with white background
        Color[] fillColor = new Color[textureWidth * textureHeight];
        for (int i = 0; i < fillColor.Length; i++) fillColor[i] = Color.white;
        graphTexture.SetPixels(fillColor);
        graphTexture.Apply();

        // Assign to GraphImage
        GraphImage.sprite = Sprite.Create(graphTexture, new Rect(0, 0, textureWidth, textureHeight), new Vector2(0.5f, 0.5f));
        GraphSprite = GraphImage.sprite;
    }

    public void GraphValues(float[] values)
    {
        GraphValues = values;
        Graph();
    }

    private void Graph()
    {
        float maxValue = Mathf.Max(GraphValues);

        float[] remapedValues = new float[GraphValues.Length];

        for (int i = 0; i < GraphValues.Length; i++)
        {
            remapedValues[i] = GraphValues[i] / maxValue * YScale;
        }

        float xStep = XScale / (GraphValues.Length - 1);
        float xOffset = -XScale / 2f;
        float yOffset = -YScale / 2f;

        for (int i = 0; i < remapedValues.Length - 1; i++)
        {
            Vector2 startPos = new Vector2(i * xStep + xOffset, remapedValues[i] + yOffset);
            Vector2 endPos = new Vector2((i + 1) * xStep + xOffset, remapedValues[i + 1] + yOffset);

            Debug.DrawLine(startPos, endPos, Color.red, 100f);
        }
    }

    private void SetPixel(float x, float y)
    {
        if (graphTexture == null) return;
        int px = Mathf.Clamp(Mathf.RoundToInt(x), 0, textureWidth - 1);
        int py = Mathf.Clamp(Mathf.RoundToInt(y), 0, textureHeight - 1);
        graphTexture.SetPixel(px, py, Color.black);
        graphTexture.Apply();
    }
}
