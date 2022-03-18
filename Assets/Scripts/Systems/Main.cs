using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;
using Newtonsoft.Json;
using System.Linq;
using Commons.CollectionCommon;

public class Main : SingletonBehaviour<Main>
{
    const string COIN_KEY = "PLAYER_GOLD";
    const string BOUGHT_KEY = "BOUGHT_KEY";
    const string WEARING_KEY = "WEARING_KEY";


    [SerializeField]
    int startCoin = 100;
    [SerializeField]
    Canvas mainCanvas;
    [SerializeField]
    CanvasGroup loadingCanvas;
    [SerializeField]
    public List<Accessory> accessoryList;

    protected override bool isUnique { get => true; }
    
    #region getter setter
    static int _coin;
    // set ti?n
    public static int coin
    {
        get => _coin;
        set
        {
            if (Home.Instance.gameObject.activeInHierarchy)
                Home.Instance.coin.text = value.ToString();
            _coin = value;
            PlayerPrefs.SetInt(COIN_KEY, value);
        }
    }

    static List<string> _boughtAccessory;
    // getter c?a c?c ph? ki?n ?? mua
    public static List<string> boughtAccessory
    {
        get => _boughtAccessory;
    }
    // method call ?? mua ph? ki?n
    public static void Buy(Accessory accessory)
    {
        coin -= accessory.price; // tr? ti?n mua
        boughtAccessory.Add(accessory.name);
        PlayerPrefs.SetString(BOUGHT_KEY, JsonConvert.SerializeObject(boughtAccessory));
    }
    static Dictionary<string, string> _wearingList = new Dictionary<string, string>();
    public static Dictionary<string, string> wearingList
    {
        get => _wearingList;
    }
    public static Accessory GetWearing(string accessoryType)
    {
        return Instance.accessoryList.Find(x => x.name == wearingList.GetValue(accessoryType));
    }
    public static void Wear(Accessory accessory)
    {
        wearingList[accessory.accessoryType] = accessory.name;
        PlayerPrefs.SetString(WEARING_KEY, JsonConvert.SerializeObject(wearingList));
    }
    public static void Remove(string accessoryType)
    {
        wearingList.Remove(accessoryType);
        PlayerPrefs.SetString(WEARING_KEY, JsonConvert.SerializeObject(wearingList));
    }
    #endregion getter setter


    private void Start()
    {
        DontDestroyOnLoad(this);

        // loadCoin
        coin = PlayerPrefs.GetInt(COIN_KEY, startCoin);
        //load bought acc
        string jsonBought = PlayerPrefs.GetString(BOUGHT_KEY, "[]");
        _boughtAccessory = JsonConvert.DeserializeObject<List<string>>(jsonBought);
        // load wearing
        string jsonWearing = PlayerPrefs.GetString(WEARING_KEY, "{}");
        _wearingList = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonWearing);

    }


    public void BackToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void InitUI(GameObject gameObject)
    {
        Instantiate(gameObject, mainCanvas.transform);
    }

    // loadScene mechanic

    public static void LoadScene(string sceneName, Action action = null)
    {
        Instance.StartCoroutine(ILoadScene(sceneName, action));
    }

    static IEnumerator ILoadScene(string sceneName, Action action)
    {
        Time.timeScale = 0;
        Instance.loadingCanvas.gameObject.SetActive(true);
        Tweener loadingAnim = Instance.loadingCanvas.DOFade(1, 2).SetUpdate(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Instance.loadingCanvas.DOFade(0, 0.5f).OnComplete(() =>
        {
            Instance.loadingCanvas.gameObject.SetActive(false);
            if (action != null)
                action();
            Time.timeScale = 1;

        }).SetUpdate(true);


        // stop anim
    }

    public static void SetTimeout(Action action, float delay)
    {
        Instance.StartCoroutine(ITimeout(action, delay));
    }
    static IEnumerator ITimeout(Action action, float delay)
    {

        yield return new WaitForSecondsRealtime(delay);
        action();

        // stop anim
    }

}
