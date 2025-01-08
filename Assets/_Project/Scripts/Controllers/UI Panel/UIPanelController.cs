using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    [HideInInspector] public static UIPanelController Instance;

    [Header("References")]
    [SerializeField] private int StartPanelIndex = 0;
    [SerializeField] private GameObject[] Panels;

    private void Awake() => Instance = this;

    private void Start() => ShowPanel(StartPanelIndex);

    public void ShowPanel(int index)
    {
        foreach (var panel in Panels) panel.SetActive(false);
        Panels[index].SetActive(true);
    }
}
