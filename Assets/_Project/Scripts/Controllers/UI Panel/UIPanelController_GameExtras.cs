using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIPanelController_GameExtras : MonoBehaviour
{
    [HideInInspector] private bool paused = false;

    public void RESTART_ButtonPressed() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void MENU_ButtonPressed()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PAUSE_Input(InputAction.CallbackContext ctx)
    {
        if(!ctx.performed) return;
        if(paused) Resume(); else Pause();
    }
    public void RESUME_ButtonPressed() => Resume();

    private void Pause(){
        Time.timeScale = 0;
        paused = true;

        UIPanelController.Instance.ShowPanel(2);
    }

    private void Resume(){
        Time.timeScale = 1;
        paused = false;

        UIPanelController.Instance.ShowPanel(0);
    }
}
