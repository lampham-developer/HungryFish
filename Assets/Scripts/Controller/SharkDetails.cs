using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SharkDetails : MonoBehaviour
{
    private static SharkDetails sharkDetailsSingleton;

    private float currentLevel = 1;
    private float healthPer = 10;
    private float speedUp = 0.5f;
    private float sizeUp = 0.025f;

    private float baseHealth = 100;
    private float baseMovementSpeed = 5;
    private float baseSize = 1f;

    private float baseExp = 10;
    private float requiredExp = 0;
    private float requiredExpUp = 10;

    private float currentExp = 0;
    public TextMeshProUGUI lvlTxt;

    private float currentGold = 0;
    private float bonusHp = 0;
    private float bonusSpeed = 0;

    public Slider expSlider;

    private void Awake()
    {
        sharkDetailsSingleton = this;
        
    }

    private void Start()
    {
        sharkDetailsSingleton = this;
        loadData();
    }

    public static SharkDetails getInstance()
    {
        return sharkDetailsSingleton;
    }

    private void loadData()
    {
        currentLevel = LocalDataController.getInstance().getSharkLevel();
        requiredExp = baseExp + requiredExpUp * currentLevel;
        lvlTxt.text = currentLevel.ToString();

        //currentGold = LocalDataController.getInstance().getPlayerGold();
        bonusHp = LocalDataController.getInstance().getBonusHp();
        bonusSpeed = LocalDataController.getInstance().getBonusSpeed();
        currentExp = LocalDataController.getInstance().getCurrentExp();

        //Debug.Log("Load" + currentGold);

        updateExpSlider();
        CharacterController.getInstance().loadLevelShark(getMaxSpeed(), getMaxHealth(), getMaxSize());
    }

    public float getMaxHealth()
    {
        return baseHealth + currentLevel * healthPer + bonusHp;
    }

    public float getMaxSpeed()
    {
        return baseMovementSpeed + currentLevel * speedUp + bonusSpeed;
    }

    public float getMaxSize()
    {
        return baseSize + currentLevel * sizeUp;
    }

    public void increaseExp(float exp)
    {
        currentExp += exp;
        expSlider.value = currentExp;
        calculateExp();
    }

    private void calculateExp()
    {
        if(currentExp >= requiredExp)
        {
            currentLevel++;
            lvlTxt.text = currentLevel.ToString();
            currentExp -= requiredExp;
            requiredExp = baseExp + currentLevel * requiredExpUp;

            updateExpSlider();
            CharacterController.getInstance().upLevelShark(getMaxSpeed(), getMaxHealth(), getMaxSize());
        }
    }

    private void updateExpSlider()
    {
        expSlider.value = currentExp;
        expSlider.maxValue = requiredExp;
    }

    public void increaseGold(int gold)
    {
        //currentGold += gold;
        //Debug.Log(currentGold);
        Main.coin += gold;
    }

    public void saveData()
    {
        LocalDataController.getInstance().setSharkLevel(currentLevel);
        //LocalDataController.getInstance().setPlayerGold(currentGold);

        //Debug.Log("Saved" + currentGold);
    }
}
