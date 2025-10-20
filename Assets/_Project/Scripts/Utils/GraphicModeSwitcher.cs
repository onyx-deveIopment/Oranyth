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
        HGM_Object.SetActive(!LGM_Enabled);
        LGM_Object.SetActive(LGM_Enabled);
    }
}
