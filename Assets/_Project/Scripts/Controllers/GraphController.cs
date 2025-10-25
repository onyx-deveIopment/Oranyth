using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    [HideInInspector] public static GraphController Instance;

    [Header("References")]
    [SerializeField] private Image GraphImage;

    private void Awake() => Instance = this;

    public void GraphValues(float[] values)
    {
        if (GraphImage == null || values == null || values.Length == 0)
        {
            Debug.LogWarning("GraphController: Missing GraphImage or values array.");
            return;
        }

        // Get the image rect dimensions
        RectTransform rect = GraphImage.rectTransform;
        int width = Mathf.RoundToInt(rect.rect.width);
        int height = Mathf.RoundToInt(rect.rect.height);

        // Create a new texture the same size as the image
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        // Fill background with transparent pixels
        Color32[] clearPixels = new Color32[width * height];
        for (int i = 0; i < clearPixels.Length; i++)
            clearPixels[i] = new Color32(0, 0, 0, 0);
        texture.SetPixels32(clearPixels);

        // Find min/max for scaling
        float minValue = Mathf.Min(values);
        float maxValue = Mathf.Max(values);
        float range = Mathf.Max(0.0001f, maxValue - minValue); // Avoid divide-by-zero

        // Scale values to fit height
        int prevX = 0;
        int prevY = Mathf.RoundToInt(((values[0] - minValue) / range) * (height - 1));

        for (int i = 1; i < values.Length; i++)
        {
            float t = (float)i / (values.Length - 1);
            int x = Mathf.RoundToInt(t * (width - 1));
            int y = Mathf.RoundToInt(((values[i] - minValue) / range) * (height - 1));

            DrawLine(texture, prevX, prevY, x, y, Color.black);

            prevX = x;
            prevY = y;
        }

        texture.Apply();

        // Create sprite and assign it to the Image
        Sprite graphSprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        GraphImage.sprite = graphSprite;
    }

    // Simple Bresenham-style line drawer
    private void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color color)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (x0 >= 0 && x0 < tex.width && y0 >= 0 && y0 < tex.height)
                tex.SetPixel(x0, y0, color);

            if (x0 == x1 && y0 == y1) break;

            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }
}
