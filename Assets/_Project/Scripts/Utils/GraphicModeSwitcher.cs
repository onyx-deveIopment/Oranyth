using UnityEngine;

public class GraphicModeSwitcher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject HGM_Object;
    [SerializeField] private GameObject LGM_Object;

    [Header("Settings")]
    [SerializeField] private bool LGM_Enabled;

    private void Start()
    {
        HGM_Object.SetActive(GraphicsMode.Instance.GetGM() != GraphicsMode.GraphicMode.Low);
        LGM_Object.SetActive(GraphicsMode.Instance.GetGM() == GraphicsMode.GraphicMode.Low);
    }
}
