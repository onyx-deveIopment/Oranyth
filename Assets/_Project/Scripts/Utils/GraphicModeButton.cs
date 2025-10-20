using TMPro;
using UnityEngine;

public class GraphicModeButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text Text;

    private void Update()
    {
        Text.text = "Graphics Mode: " + GraphicsMode.Instance.GetGM().ToString();
    }

    public void GM_ButtonPressed() => GraphicsMode.Instance.ToggleGM();
}
