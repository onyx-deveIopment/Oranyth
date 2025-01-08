using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelController_GameExtras : MonoBehaviour
{
    public void RESTART_ButtonPressed() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void MENU_ButtonPressed() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
}
