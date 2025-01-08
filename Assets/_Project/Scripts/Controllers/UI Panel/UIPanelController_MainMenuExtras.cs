using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelController_MainMenuExtras : MonoBehaviour
{
    public void START_ButtonPressed() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void EXIT_ButtonPressed() => Application.Quit();
}
