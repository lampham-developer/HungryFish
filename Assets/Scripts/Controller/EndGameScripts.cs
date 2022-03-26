using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameScripts : PopupBase
{
    private float score = 0;
    public TextMeshProUGUI scoreTxt;

    public EndGameScripts(float score)
    {
        this.score = score;
    }

    private void Start()
    {
        scoreTxt.text = GameController.GameControllerSingleton.getScore().ToString();
    }

    public void RestartScene()
    {
        Close(() =>
        {
            //Time.timeScale = 1;
            Main.LoadScene(SceneManager.GetActiveScene().name);

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
