using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIPanelController_GameExtras : MonoBehaviour
{
    [HideInInspector] private bool paused = false;

    public void RESTART_ButtonPressed() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void MENU_ButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PAUSE_Input(InputAction.CallbackContext ctx)
    {
        if(!ctx.performed) return;
        if(GameController.Instance.IsGameOver) return;
        if (paused) Resume(); else Pause();
    }
    public void RESUME_ButtonPressed() => Resume();

    private void Pause(){
        Time.timeScale = 0;
        paused = true;

#if !UNITY_EDITOR
        if(!Application.version.Contains("andrewarcade"))
            Cursor.visible = true;
#endif

        UIPanelController.Instance.ShowPanel(2);

#if !UNITY_EDITOR
        if(Application.version.Contains("poki"))
            PokiUnitySDK.Instance.gameplayStop();
#endif
    }

    private void Resume(){
        Time.timeScale = 1;
        paused = false;

#if !UNITY_EDITOR
        if(!Application.version.Contains("andrewarcade"))
            Cursor.visible = false;
#endif

        UIPanelController.Instance.ShowPanel(0);

#if !UNITY_EDITOR
        if(Application.version.Contains("poki"))
            PokiUnitySDK.Instance.gameplayStart();
#endif
    }
}
