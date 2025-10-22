using UnityEngine;

public class GM_GraphicsSwitcher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject HGM_Object;
    [SerializeField] private GameObject LGM_Object;

    private void Update()
    {
        HGM_Object.SetActive(GraphicsMode.Instance.GetGM() != GraphicsMode.GraphicMode.Low);
        LGM_Object.SetActive(GraphicsMode.Instance.GetGM() == GraphicsMode.GraphicMode.Low);
    }
}
