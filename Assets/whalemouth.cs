using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whalemouth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        if (hitInfo.gameObject.tag == "SharkMounth")
        {
            
            transform.parent.gameObject.GetComponent<KillerWhale>().hp-= CharacterController.CharacterSingleton.damage*.5f;
            CharacterController.CharacterSingleton.decreaseHealth(transform.parent.gameObject.GetComponent<KillerWhale>().damage*.5f);
         
        }
        if (hitInfo.gameObject.tag == "SharkBody")
        {
            
            
            CharacterController.CharacterSingleton.decreaseHealth(transform.parent.gameObject.GetComponent<KillerWhale>().damage);
            
        }
    }
}
