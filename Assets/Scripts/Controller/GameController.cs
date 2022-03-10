using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController GameControllerSingleton;
    [SerializeField]
    GameObject pauseMenuPopupTemplate;
     public int current_fish =0;
     public int current_mine =0;

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
}
