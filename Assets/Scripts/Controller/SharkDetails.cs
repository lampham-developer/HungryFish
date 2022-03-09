using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDetails : MonoBehaviour
{
    public static SharkDetails sharkDetailsSingleton;

    private int currentLevel = 1;
    private float healthPer = 10;
    private float speedUp = 1;
    private float sizeUp = 0.1f;

    private float baseHealth = 100;
    private float baseMovementSpeed = 5;
    private float baseSize = 1f;

    private float baseExp = 10;
    private float requiredExp = 0;
    private float requiredExpUp = 10;

    private float currentExp = 0;

    private void Awake()
    {
        loadLevel();
        sharkDetailsSingleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadLevel()
    {
        //todo load current level from local data
        requiredExp = baseExp + requiredExpUp * currentLevel;
    }

    public float getMaxHealth()
    {
        return baseHealth + currentLevel * healthPer;
    }

    public float getMaxSpeed()
    {
        return baseMovementSpeed + currentLevel * speedUp;
    }

    public float getMaxSize()
    {
        return baseSize + currentLevel * sizeUp;
    }

    public void increaseExp(float exp)
    {
        currentExp += exp;
        Debug.Log(currentExp);
        calculateExp();
    }

    private void calculateExp()
    {
        if(currentExp >= requiredExp)
        {
            Debug.Log("lvl up");
            currentLevel++;
            currentExp -= requiredExp;
            requiredExp = baseExp + currentLevel * requiredExpUp;

            CharacterController.CharacterSingleton.upLevelShark(getMaxSpeed(), getMaxHealth(), getMaxSize());
        }
    }
}
