using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    [HideInInspector] public static GraphController Instance;

    [Header("References")]
    [SerializeField] private Image GraphImage;

    [Header("Appearance Settings")]
    [SerializeField] private Color GraphColor = Color.white;
    [SerializeField] private Color BorderColor = Color.black;
    [SerializeField] private Color BackgroundColor = Color.clear;
    [Range(1, 10)]
    [SerializeField] private int LineThickness = 2;
    [Range(0, 10)]
    [SerializeField] private int BorderThickness = 4;

    [Header("Performance Settings")]
    [Tooltip("Fraction of the UI Image resolution to render at (e.g., 0.5 = half res).")]
    [Range(0.1f, 1f)]
    [SerializeField] private float ResolutionScale = 1f;

    [Tooltip("Automatically skip data so that there is roughly one point per horizontal pixel.")]
    [SerializeField] private bool AutoDownsample = true;

    [Tooltip("Fraction of values to keep (1 = all, 0.1 = every 10th value). Auto-skipped if left at 1.")]
    [Range(0.01f, 1f)]
    [SerializeField] private float DataFraction = 1f;

    [Tooltip("Preserve min/max per pixel step (true = more accurate but slower).")]
    [SerializeField] private bool PreserveExtremes = true;

    private void Awake() => Instance = this;

    public void GraphValues(float[] values)
    {
        if (GraphImage == null || values == null || values.Length == 0)
        {
            Debug.LogWarning("GraphController: Missing GraphImage or values array.");
            return;
        }

        // Get image rect and apply scale
        RectTransform rect = GraphImage.rectTransform;
        int fullWidth = Mathf.RoundToInt(rect.rect.width);
        int fullHeight = Mathf.RoundToInt(rect.rect.height);
        int width = Mathf.Max(1, Mathf.RoundToInt(fullWidth * ResolutionScale));
        int height = Mathf.Max(1, Mathf.RoundToInt(fullHeight * ResolutionScale));

        if (width <= 0 || height <= 0)
        {
            Debug.LogWarning("GraphController: Invalid scaled GraphImage size.");
            return;
        }

        // Create texture + background buffer
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Bilinear;
        Color32[] pixels = new Color32[width * height];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = BackgroundColor;

        // Scale values
        float minValue = Mathf.Min(values);
        float maxValue = Mathf.Max(values);
        float range = Mathf.Max(0.0001f, maxValue - minValue);

        // Determine step size based on settings
        int autoStep = AutoDownsample ? Mathf.Max(1, values.Length / width) : 1;
        int manualStep = Mathf.Max(1, Mathf.RoundToInt(1f / Mathf.Clamp(DataFraction, 0.001f, 1f)));
        int step = Mathf.Max(autoStep, manualStep);

        int prevX = 0;
        int prevY = Mathf.RoundToInt(((values[0] - minValue) / range) * (height - 1));

        // Core draw loop (continuous)
        for (int i = step; i < values.Length; i += step)
        {
            int x = Mathf.RoundToInt((float)i / (values.Length - 1) * (width - 1));
            int y = Mathf.RoundToInt(((values[i] - minValue) / range) * (height - 1));

            if (PreserveExtremes)
            {
                // Find local min/max in skipped range
                float localMin = float.MaxValue;
                float localMax = float.MinValue;
                int end = Mathf.Min(i + step, values.Length);
                for (int j = i; j < end; j++)
                {
                    if (values[j] < localMin) localMin = values[j];
                    if (values[j] > localMax) localMax = values[j];
                }
                int minY = Mathf.RoundToInt(((localMin - minValue) / range) * (height - 1));
                int maxY = Mathf.RoundToInt(((localMax - minValue) / range) * (height - 1));

                // Draw vertical spike range (for extremes)
                DrawBorderedLineBuffer(pixels, width, height, x, minY, x, maxY, GraphColor, BorderColor, LineThickness, BorderThickness);

                // Ensure continuous connection from previous point
                DrawBorderedLineBuffer(pixels, width, height, prevX, prevY, x, y, GraphColor, BorderColor, LineThickness, BorderThickness);
            }
            else
            {
                // Continuous connection
                DrawBorderedLineBuffer(pixels, width, height, prevX, prevY, x, y, GraphColor, BorderColor, LineThickness, BorderThickness);
            }

            prevX = x;
            prevY = y;
        }

        // Apply final pixels in one batch
        texture.SetPixels32(pixels);
        texture.Apply(false);

        // Display
        GraphImage.sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
    }

    // Faster version using a pixel buffer (no GetPixel)
    private void DrawBorderedLineBuffer(Color32[] buffer, int width, int height,
        int x0, int y0, int x1, int y1, Color innerColor, Color borderColor, int thickness, int border)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        int outerRadius = (thickness / 2) + border;
        int innerRadius = thickness / 2;

        while (true)
        {
            DrawThickPixelBuffer(buffer, width, height, x0, y0, innerColor, borderColor, innerRadius, outerRadius);

            if (x0 == x1 && y0 == y1) break;

            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }

    private void DrawThickPixelBuffer(Color32[] buffer, int width, int height,
        int cx, int cy, Color innerColor, Color borderColor, int innerRadius, int outerRadius)
    {
        int outerSq = outerRadius * outerRadius;
        int innerSq = innerRadius * innerRadius;

        for (int x = -outerRadius; x <= outerRadius; x++)
        {
            int px = cx + x;
            if (px < 0 || px >= width) continue;

            for (int y = -outerRadius; y <= outerRadius; y++)
            {
                int py = cy + y;
                if (py < 0 || py >= height) continue;

                int idx = px + py * width;
                int distSq = x * x + y * y;

                if (distSq <= outerSq)
                {
                    if (distSq > innerSq)
                    {
                        if (buffer[idx].a == 0)
                            buffer[idx] = borderColor;
                    }
                    else
                    {
                        buffer[idx] = innerColor;
                    }
                }
            }
        }
    }
}
