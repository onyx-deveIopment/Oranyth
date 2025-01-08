using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] Panels;

    private void Start() => ShowPanel(0);

    public void ShowPanel(int index)
    {
        foreach (var panel in Panels) panel.SetActive(false);
        Panels[index].SetActive(true);
    }
}
