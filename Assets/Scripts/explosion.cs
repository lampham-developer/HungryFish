using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update
    Timer timer;
    
    AudioSource audio;
    void Start()
    {
        timer =gameObject.AddComponent<Timer>();
        timer.Duration=1f;
        timer.Run();
         audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { if(timer.Finished){
        Destroy(gameObject);

    }
        
    }
}
