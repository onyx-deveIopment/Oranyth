using UnityEngine;

public class GraphicsMode : MonoBehaviour
{
    [HideInInspector] public static GraphicsMode Instance;

    [Header("Settings")]
    [SerializeField] private GraphicMode GM = GraphicMode.High;

    public enum GraphicMode { High, Low }

    private void Awake() => Setup();

    private void Setup()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("GraphicMode")) GM = (GraphicMode)PlayerPrefs.GetInt("GraphicMode");
    }

    public void SaveGM() => PlayerPrefs.SetInt("GraphicMode", (int)GM);

    public GraphicMode GetGM() => GM;
    public void SetGM(GraphicMode _GM)
    {
        GM = _GM;
        SaveGM();
    }

    public void ToggleGM()
    {
        switch (GM)
        {
            case GraphicMode.High: GM = GraphicMode.Low; break;
            case GraphicMode.Low: GM = GraphicMode.High; break;
        }
        SaveGM();
    }
}
