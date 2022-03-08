using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDetails : MonoBehaviour
{

    private int currentLevel = 1;
    private float healthPer = 10;
    private float speedUp = 1;
    private float sizeUp = 0.1f;

    private float baseHealth = 100;
    private float baseMovementSpeed = 5;
    private float baseSize = 0.5f;

    private float requiredExp = 100;
    private float requiredExpUp = 10;

    private void Awake()
    {
        loadLevel();
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
        requiredExp = requiredExp + requiredExpUp * currentLevel;
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


}
