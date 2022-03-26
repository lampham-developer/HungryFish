using System.Collections;
using TMPro;
using UnityEngine;


public class EndgamePopup : PauseMenu // thực tế thì method y như PauseMenu
{
    [SerializeField]
    TMP_Text txtScore, txtHighScore;
    public void SetScore(float score, float highScore)
    {
        txtScore.text = score.ToString();
        txtHighScore.text = highScore.ToString();
    }

}
