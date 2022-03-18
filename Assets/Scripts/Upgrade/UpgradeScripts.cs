using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeScripts : MonoBehaviour
{
    private float currentCoin = 0;
    private float currentBonusHp = 0;
    private float currentBonusSpeed = 0;



    private void Awake()
    {
        loadData();
    }

    private void loadData()
    {
        currentCoin = LocalDataController.getInstance().getPlayerGold();
        currentBonusHp = LocalDataController.getInstance().getBonusHp();
        currentBonusSpeed = LocalDataController.getInstance().getBonusSpeed();
    }

    public void goBack()
    {
    }

    public void upgradeSpeed()
    {

    }

    public void upgradeHp()
    {

    }

    private void setActiveStatus(bool status)
    {

    }

    private void setUpgradeHpPrice()
    {

    }

    private void setUpgradeSpeedPrice()
    {

    }

    private void setUpgradeHpIcon()
    {

    }

    private void setUpgradeSpeedIcon()
    {

    }
}
