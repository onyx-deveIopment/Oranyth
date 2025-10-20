using UnityEngine;

public class GM_PostProcessing : MonoBehaviour
{
    private void Start() => gameObject.SetActive(GraphicsMode.Instance.GetGM() != GraphicsMode.GraphicMode.Low);
}
