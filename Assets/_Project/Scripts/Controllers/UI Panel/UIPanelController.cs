using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanelController : MonoBehaviour
{
    [HideInInspector] public static UIPanelController Instance;

    [Header("References")]
    [SerializeField] private int StartPanelIndex = 0;
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private GameObject[] PanelSelects;

    private void Awake() => Instance = this;

    private void Start() => ShowPanel(StartPanelIndex);

    public void ShowPanel(int index)
    {
        foreach (var panel in Panels) panel.SetActive(false);
        if(PanelSelects[index] != null) EventSystem.current.SetSelectedGameObject(PanelSelects[index]);
        Panels[index].SetActive(true);
    }
}
