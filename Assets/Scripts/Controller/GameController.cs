using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController GameControllerSingleton;
    [SerializeField]
    GameObject pauseMenuPopupTemplate;

    void Awake()
    {
        GameControllerSingleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endGame()
    {
        Time.timeScale = 0f;
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

    public void exitGame()
    {

    }

    public void scoreUp(float score)
    {

        //todo set text for score label

        SharkDetails.sharkDetailsSingleton.increaseExp(score);
    }
}
