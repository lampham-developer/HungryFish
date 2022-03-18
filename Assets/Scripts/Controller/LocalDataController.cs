using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDataController : MonoBehaviour
{
    public static LocalDataController LocalDataControllerSingleton;

    static string PLAYER_GOLD = "PLAYER_GOLD";
    static string SHARK_LEVEL = "SHARK_LEVEL";
    static string HIGH_SCORE = "HIGH_SCORE";
    static string BONUS_HP = "BONUS_HP";
    static string BONUS_SPEED = "BONUS_SPEED";

    private void Awake()
    {
        LocalDataControllerSingleton = this;
    }

    public static LocalDataController getInstance()
    {
        return LocalDataControllerSingleton;
    }

    public float getPlayerGold()
    {
        if (PlayerPrefs.HasKey(PLAYER_GOLD))
        {
            return PlayerPrefs.GetFloat(PLAYER_GOLD);
        }
        else return 0;
    }

    public void setPlayerGold(float gold)
    {
         PlayerPrefs.SetFloat(PLAYER_GOLD, gold);
    }

    public float getSharkLevel()
    {
        if (PlayerPrefs.HasKey(SHARK_LEVEL))
        {
            return PlayerPrefs.GetFloat(SHARK_LEVEL);
        }
        else return 1;
    }

    public void setSharkLevel(float level)
    {
        PlayerPrefs.SetFloat(SHARK_LEVEL, level);
    }

    public float getHighScore()
    {
        if (PlayerPrefs.HasKey(HIGH_SCORE))
        {
            return PlayerPrefs.GetFloat(HIGH_SCORE);
        }
        else return 0;
    }

    public void setHighScore(float score)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE, score);
    }

    public float getBonusHp()
    {
        if (PlayerPrefs.HasKey(BONUS_HP))
        {
            return PlayerPrefs.GetFloat(BONUS_HP);
        }
        else return 0;
    }

    public void setBonusHp(float bonus)
    {
        PlayerPrefs.SetFloat(BONUS_HP, bonus);
    }

    public float getBonusSpeed()
    {
        if (PlayerPrefs.HasKey(BONUS_SPEED))
        {
            return PlayerPrefs.GetFloat(BONUS_SPEED);
        }
        else return 0;
    }

    public void setBonusSpeed(float bonus)
    {
        PlayerPrefs.SetFloat(BONUS_SPEED, bonus);
    }

}
