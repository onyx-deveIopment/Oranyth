using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

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

    #region Graph Rendering

    public Texture2D GraphValues(float[] values, bool forExport = false, int exportWidth = 800, int exportHeight = 400)
    {
        if (values == null || values.Length == 0)
        {
            Debug.LogWarning("GraphController: Missing values array.");
            return null;
        }

        int width, height;
        if (forExport)
        {
            width = Mathf.Max(1, exportWidth);
            height = Mathf.Max(1, exportHeight);
        }
        else
        {
            if (GraphImage == null)
            {
                Debug.LogWarning("GraphController: GraphImage reference missing.");
                return null;
            }
            RectTransform rect = GraphImage.rectTransform;
            int fullWidth = Mathf.RoundToInt(rect.rect.width);
            int fullHeight = Mathf.RoundToInt(rect.rect.height);
            width = Mathf.Max(1, Mathf.RoundToInt(fullWidth * ResolutionScale));
            height = Mathf.Max(1, Mathf.RoundToInt(fullHeight * ResolutionScale));
        }

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Bilinear;
        Color32[] pixels = new Color32[width * height];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = BackgroundColor;

        float minValue = Mathf.Min(values);
        float maxValue = Mathf.Max(values);
        float range = Mathf.Max(0.0001f, maxValue - minValue);

        int autoStep = AutoDownsample ? Mathf.Max(1, values.Length / width) : 1;
        int manualStep = Mathf.Max(1, Mathf.RoundToInt(1f / Mathf.Clamp(DataFraction, 0.001f, 1f)));
        int step = Mathf.Max(autoStep, manualStep);

        int prevX = 0;
        int prevY = Mathf.RoundToInt(((values[0] - minValue) / range) * (height - 1));

        for (int i = step; i < values.Length; i += step)
        {
            int x = Mathf.RoundToInt((float)i / (values.Length - 1) * (width - 1));
            int y = Mathf.RoundToInt(((values[i] - minValue) / range) * (height - 1));

            if (PreserveExtremes)
            {
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

                DrawBorderedLineBuffer(pixels, width, height, x, minY, x, maxY, GraphColor, BorderColor, LineThickness, BorderThickness);
                DrawBorderedLineBuffer(pixels, width, height, prevX, prevY, x, y, GraphColor, BorderColor, LineThickness, BorderThickness);
            }
            else
            {
                DrawBorderedLineBuffer(pixels, width, height, prevX, prevY, x, y, GraphColor, BorderColor, LineThickness, BorderThickness);
            }

            prevX = x;
            prevY = y;
        }

        texture.SetPixels32(pixels);
        texture.Apply(false);

        if (!forExport && GraphImage != null)
        {
            GraphImage.sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }

        return texture;
    }

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

    #endregion

    #region Shareable URL / Compressed Graph Data

    /// <summary>
    /// Returns a compressed Base64 string of the graph coordinates.
    /// Each point: x,y as ushort (2 bytes each) relative to export resolution.
    /// </summary>
    public string GetCompressedGraphData(float[] values, int exportWidth = 800, int exportHeight = 400)
    {
        if (values == null || values.Length == 0) return null;

        float minValue = Mathf.Min(values);
        float maxValue = Mathf.Max(values);
        float range = Mathf.Max(0.0001f, maxValue - minValue);

        // Downsample to 1-2 points per pixel
        int step = Mathf.Max(1, values.Length / exportWidth);

        using (MemoryStream ms = new MemoryStream())
        {
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                for (int i = 0; i < values.Length; i += step)
                {
                    ushort x = (ushort)Mathf.RoundToInt((float)i / (values.Length - 1) * (exportWidth - 1));
                    ushort y = (ushort)Mathf.RoundToInt((values[i] - minValue) / range * (exportHeight - 1));
                    bw.Write(x);
                    bw.Write(y);
                }
            }

            // Compress with GZip
            byte[] rawBytes = ms.ToArray();
            using (MemoryStream compressedMs = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(compressedMs, CompressionMode.Compress))
                {
                    gzip.Write(rawBytes, 0, rawBytes.Length);
                }
                byte[] compressed = compressedMs.ToArray();
                return Convert.ToBase64String(compressed);
            }
        }
    }

    /// <summary>
    /// Generates a shareable URL with compressed data and copies it to clipboard.
    /// </summary>
    public string GetGraphShareURL(float[] values, int exportWidth = 800, int exportHeight = 400)
    {
        string compressedData = GetCompressedGraphData(values, exportWidth, exportHeight);
        if (string.IsNullOrEmpty(compressedData)) return null;

        string url = $"https://onyx.andrewcromar.org/oranythgraph.php?data={Uri.EscapeDataString(compressedData)}";
        GUIUtility.systemCopyBuffer = url;
        return url;
    }

    #endregion
}
