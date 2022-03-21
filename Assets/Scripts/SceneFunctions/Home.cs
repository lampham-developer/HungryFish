using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Commons;
using DG.Tweening;

public class Home : SingletonBehaviour<Home>
{
    [SerializeField]
    public TMP_Text coin;
    [SerializeField]
    RectTransform btnGroup,shopPanel;

    [SerializeField]
    float duration = 0.5f;



    public void GoToPlay()
    {
        Main.LoadScene("GameScene");
    }

    private void Start()
    {
        coin.text = Main.coin.ToString();
    }

    public void GoToShop()
    {
        btnGroup.DOPivotY(1, duration);
        shopPanel.gameObject.SetActive(true);
        shopPanel.localScale = Vector2.up;
        shopPanel.DOScaleX(1, duration);
    }
    public void BackToHome()
    {
        btnGroup.DOPivotY(0, duration);
        shopPanel.DOScaleX(0, duration).OnComplete(()=>
        {
            shopPanel.gameObject.SetActive(false);
        });
    }

    public void goToUpgrade()
    {
        Main.LoadScene("UpgradeScene");
    }
}
