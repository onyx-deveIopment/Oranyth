using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] Panels;
    // 0 = Title,
    // 1 = Instructions,
    // 2 = Credits

    private void Start() => ShowPanel(0);

    public void StartButtonPressed() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void ExitButtonPressed() => Application.Quit();

    public void ShowPanel(int index)
    {
        foreach (var panel in Panels) panel.SetActive(false);
        Panels[index].SetActive(true);
    }
}
