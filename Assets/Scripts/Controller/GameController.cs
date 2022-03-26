using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController GameControllerSingleton;
    [SerializeField]

    public GameObject pauseMenuPopupTemplate;

    public int current_fish =0;
    public int current_mine =0;

    private float currentScore = 0;
    public TextMeshProUGUI scoreTxt;

    private float highScore = 0;

    private bool isGameEnded = false;
    public GameObject endMenuPopupTemplate;


    void Awake()
    {
        GameControllerSingleton = this;
    }

    public GameController getInstance()
    {
        return GameControllerSingleton;
    }

    public void endGame()
    {
        Time.timeScale = 0f;
        highScore = LocalDataController.getInstance().getHighScore();
        saveData();
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        Main.Instance.InitUI(pauseMenuPopupTemplate);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    public void scoreUp(float score)
    {
        currentScore += score;
        scoreTxt.text = currentScore.ToString();
        SharkDetails.getInstance().increaseExp(score);
        SharkDetails.getInstance().increaseGold(score);
    }
    public void spawnFish(int n){
        current_fish+=n;
    }
    public void removeFish(){
        current_fish--;
    }
    public void spawnMine(){
        current_mine++;
    }
    public void removeMine(){
        current_mine--;
    }

    private void saveData()
    {
        if (!isGameEnded)
        {
            if (currentScore > highScore)
            {
                LocalDataController.getInstance().setHighScore(currentScore);
            }

            SharkDetails.getInstance().saveData();

            Main.Instance.InitUI(endMenuPopupTemplate);

            isGameEnded = true;
        }
    }

    public float getScore()
    {
        return currentScore;
    }
}
