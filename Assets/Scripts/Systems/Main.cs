using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class Main : SingletonBehaviour<Main>
{
    const string COIN_KEY = "COIN_KEY";

    [SerializeField]
    int startCoin = 100;
    [SerializeField]
    Canvas mainCanvas;
    [SerializeField]
    CanvasGroup loadingCanvas;

    static int _coin;
    public static int coin
    {
        get => _coin;
        set
        {
            _coin = value;
            PlayerPrefs.SetInt(COIN_KEY, value);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this);

        // loadCoin
        _coin = PlayerPrefs.GetInt(COIN_KEY, startCoin);

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
        Tweener loadingAnim = Instance.loadingCanvas.DOFade(1, 0.5f).SetUpdate(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone || loadingAnim.IsActive())
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
