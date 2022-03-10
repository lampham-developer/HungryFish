using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    //[SerializeField]
    //Button playBtn;

    public void GoToPlay()
    {
        Main.LoadScene("GameScene");
    }
}
