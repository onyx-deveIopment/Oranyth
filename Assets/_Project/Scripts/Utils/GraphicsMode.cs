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
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("GraphicMode")) GM = (GraphicMode)PlayerPrefs.GetInt("GraphicMode");
    }

    public GraphicMode GetGM() => GM;
    public void SetGM(GraphicMode _GM) => GM = _GM;
    public void SaveGM() => PlayerPrefs.SetInt("GraphicMode", (int)GM);
}
