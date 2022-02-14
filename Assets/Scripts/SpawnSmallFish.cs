using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSmallFish : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject smallFish;
    [SerializeField]
    Camera mainCamera;
     Timer timer;
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration =3f;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
         float x1 =mainCamera.nearClipPlane;
         print(mainCamera.nearClipPlane);
        float minX = -7.9f;
        float minY = -3.9f;
        float maxX = 7.9f;
        float maxY = 3.9f;
        float x =Random.Range(minX,maxX);
        while(x <2 && x>-2){

              x =Random.Range(minX,maxX);
        }
        float y =Random.Range(minY,maxY);
        while(y <1 && x>-1){

              y =Random.Range(minY,maxY);
        }
        if(timer.Finished){
            GameObject obj = Instantiate<GameObject>(smallFish,new Vector3(x,y,0), Quaternion.identity);
            timer.Run();
        }
    }
   
}
