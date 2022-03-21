using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeScripts : MonoBehaviour
{
    private float currentCoin = 0;
    private float currentBonusHp = 0;
    private float currentBonusSpeed = 0;

    public TextMeshProUGUI currentHealthTxt;
    public TextMeshProUGUI currentSpeedTxt;
    public TextMeshProUGUI currentCoinTxt;

    public TextMeshProUGUI healthPriceTxt;
    public TextMeshProUGUI speedPriceTxt;

    private float maxUpgrade = 5;
    private float currentSpeedUpgrade = 0;
    private float currentHpUpgrade = 0;

    private float basePrice = 1000;
    private float priceMultiple = 2;

    private float speedUpStep = 4;
    private float hpUpStep = 100;

    private List<GameObject> hpBar = new List<GameObject>();
    private List<GameObject> speedBar = new List<GameObject>();


    private void Start()
    {
        loadData();
    }

    private void loadData()
    {      
        currentCoin = LocalDataController.getInstance().getPlayerGold();
        currentBonusHp = LocalDataController.getInstance().getBonusHp();
        currentBonusSpeed = LocalDataController.getInstance().getBonusSpeed();

        Debug.Log("Loaded " + currentCoin);

        currentSpeedUpgrade = currentBonusSpeed / speedUpStep;
        currentHpUpgrade = currentBonusHp / hpUpStep;
        addIcon();

        setText();
        setUpgradeHpPrice();
        setUpgradeSpeedPrice();
        setUpgradeSpeedIcon();
        setUpgradeHpIcon();
    }

    private void addIcon()
    {
        hpBar.Clear();
        speedBar.Clear();

        hpBar.Add(GameObject.Find("Hp1"));
        hpBar.Add(GameObject.Find("Hp2"));
        hpBar.Add(GameObject.Find("Hp3"));
        hpBar.Add(GameObject.Find("Hp4"));
        hpBar.Add(GameObject.Find("Hp5"));

        speedBar.Add(GameObject.Find("Speed1"));
        speedBar.Add(GameObject.Find("Speed2"));
        speedBar.Add(GameObject.Find("Speed3"));
        speedBar.Add(GameObject.Find("Speed4"));
        speedBar.Add(GameObject.Find("Speed5"));

        maxUpgrade = hpBar.Count;
    }

    public void goBack()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void upgradeSpeed()
    {
        Debug.Log(getPrice(currentSpeedUpgrade + 1) >= currentCoin);
        Debug.Log(currentSpeedUpgrade < maxUpgrade);

        if (getPrice(currentSpeedUpgrade + 1) <= currentCoin && currentSpeedUpgrade < maxUpgrade)
        {
            currentCoin -= getPrice(currentSpeedUpgrade + 1);
            currentSpeedUpgrade++;
            currentBonusSpeed = currentSpeedUpgrade * speedUpStep;

            LocalDataController.getInstance().setBonusSpeed(currentBonusSpeed);
            LocalDataController.getInstance().setPlayerGold(currentCoin);

            setText();
            setUpgradeSpeedIcon();

            Debug.Log("Speed upgrade");
        }
    }

    public void upgradeHp()
    {

        Debug.Log("clicked");
        if (getPrice(currentHpUpgrade + 1) <= currentCoin && currentHpUpgrade < maxUpgrade)
        {
            currentCoin -= getPrice(currentHpUpgrade + 1);
            currentHpUpgrade++;
            currentBonusHp = currentHpUpgrade * hpUpStep;

            LocalDataController.getInstance().setBonusHp(currentBonusHp);
            LocalDataController.getInstance().setPlayerGold(currentCoin);

            setText();
            setUpgradeHpIcon();

            Debug.Log("Hp upgrade");
        }
    }

    private void setUpgradeHpPrice()
    {
        if (currentHpUpgrade < maxUpgrade)
        {
            healthPriceTxt.text = getPrice(currentHpUpgrade + 1).ToString();

            if (getPrice(currentHpUpgrade + 1) > currentCoin)
            {
                healthPriceTxt.color = Color.red;
            }
            else healthPriceTxt.color = Color.white;
        }
        else
        {
            healthPriceTxt.text = "MAX";
            healthPriceTxt.color = Color.white;
        }
    }

    private void setUpgradeSpeedPrice()
    {
        if (currentSpeedUpgrade < maxUpgrade)
        {
            speedPriceTxt.text = getPrice(currentSpeedUpgrade + 1).ToString();

            if (getPrice(currentSpeedUpgrade + 1) > currentCoin)
            {
                speedPriceTxt.color = Color.red;
            }
            else speedPriceTxt.color = Color.white;
        }
        else {
            speedPriceTxt.text = "MAX";
            speedPriceTxt.color = Color.white;
        }
    }

    private void setUpgradeHpIcon()
    {
        for(int index = 0; index < maxUpgrade; index++)
        {
            Image image = (Image)hpBar[index].GetComponent<Image>();
            if(image != null)
            {
                if (index <= currentHpUpgrade - 1)
                {
                    image.color = Color.yellow;
                }
                else
                {
                    image.color = Color.white;
                }
            }
        }
    }

    private void setUpgradeSpeedIcon()
    {
        for (int index = 0; index < maxUpgrade; index++)
        {
            Image image = (Image)speedBar[index].GetComponent<Image>();
            if (image != null)
            {
                if (index <= currentSpeedUpgrade - 1)
                {
                    image.color = Color.yellow;
                }
                else
                {
                    image.color = Color.white;
                }
            }
        }
    }

    private float getPrice(float level)
    {
        return level * basePrice * priceMultiple;
    }

    private void setText()
    {
        currentCoinTxt.text = currentCoin.ToString();
        currentHealthTxt.text = currentBonusHp.ToString();
        currentSpeedTxt.text = currentBonusSpeed.ToString();
        setUpgradeSpeedPrice();
        setUpgradeHpPrice();
    }

    public void resetAllLocalData()
    {
        LocalDataController.getInstance().resetAllLocalData();
        loadData();
        setText();
        setUpgradeHpIcon();
        setUpgradeSpeedIcon();
    }

    public void cheatGold()
    {
        currentCoin += 1000;
        LocalDataController.getInstance().setPlayerGold(currentCoin);
        setText();
    }
}
