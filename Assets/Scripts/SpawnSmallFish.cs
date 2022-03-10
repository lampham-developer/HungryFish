using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSmallFish : MonoBehaviour
{
    //Start is called before the first frame update
    [SerializeField]
    GameObject smallFish;
    [SerializeField]
    GameObject smallFish1;
    [SerializeField]
    GameObject smallFish2;
    [SerializeField]
    Camera mainCamera;
    Timer timer;
    int max_fish = 200;
    
    
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = .5f;
        timer.Run();

    }

    // Update is called once per frame
    void Update()
    {
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float height = GetComponent<SpriteRenderer>().bounds.size.y;  


        float minX = transform.position.x - width / 2;
        float maxX =transform.position.x + width / 2;
        float maxY = transform.position.y + height / 2;
        float minY = transform.position.y -height / 2;
        
            float x = Random.Range(-100, 100);
        while (x < maxX && x > minX)
        {

            x = Random.Range(-100, 100);
        }
        float y = Random.Range(-20, 20);
       
        while (y < maxY && y > minY)
        {

            y = Random.Range(-20, 20);
        }
        if (timer.Finished)
        {
            int type = Random.Range(1, 4);
            if(GameController.GameControllerSingleton.current_fish<max_fish){
                 switch (type)
            {
                case 1:
                    GameObject obj = Instantiate<GameObject>(smallFish, new Vector3(x, y, 0), Quaternion.identity);
                    GameController.GameControllerSingleton.spawnFish(1);
                    break;
                case 2:
                    GameObject aSmallFish1 = Instantiate<GameObject>(smallFish1, new Vector3(x, y, 0), Quaternion.identity);
                    GameController.GameControllerSingleton.spawnFish(1);
                    break;
                case 3:
                    GameObject aSmallFish2 = Instantiate<GameObject>(smallFish2, new Vector3(x, y, 0), Quaternion.identity);
                    GameObject aSmallFish2_1 = Instantiate<GameObject>(smallFish2, new Vector3(x + 1, y + 1, 0), Quaternion.identity);
                    GameObject aSmallFish2_2 = Instantiate<GameObject>(smallFish2, new Vector3(x - 1, y - 1, 0), Quaternion.identity);
                    GameObject aSmallFish2_3 = Instantiate<GameObject>(smallFish2, new Vector3(x + 1, y, 0), Quaternion.identity);
                    GameController.GameControllerSingleton.spawnFish(4);
                    break;
                default: break;
            }
            }
           

            timer.Run();
        }

    }

}
