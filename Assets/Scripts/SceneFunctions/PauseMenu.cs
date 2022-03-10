using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : PopupBase
{
    public void RestartScene()
    {
        Close(() =>
        {
            //Time.timeScale = 1;
            Main.LoadScene(SceneManager.GetActiveScene().name);

        });
    }
    public void Continue()
    {
        Close(() =>
        {
            //Time.timeScale = 1;
            GameController.GameControllerSingleton.resumeGame();

        });
    }

    public void BackToHome()
    {
        Close(() =>
        {
            //Time.timeScale = 1;
            Main.LoadScene("HomeScene");

        });
    }
}
