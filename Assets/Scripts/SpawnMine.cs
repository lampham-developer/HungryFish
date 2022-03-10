using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMine : MonoBehaviour
{
    //Start is called before the first frame update
    [SerializeField]
    GameObject Mine;
    
  
    [SerializeField]
    Camera mainCamera;
    Timer timer;
    int max_mine = 50;
    
    
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1.5f;
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
            if(GameController.GameControllerSingleton.current_mine<max_mine){
          
              
                    GameObject obj = Instantiate<GameObject>(Mine, new Vector3(x, y, 0), Quaternion.identity);
                    GameController.GameControllerSingleton.spawnFish(1);
                   
               
            }
            
           

            timer.Run();
        }

    }

}

