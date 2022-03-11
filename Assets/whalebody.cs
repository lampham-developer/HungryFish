using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whalebody : MonoBehaviour
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
            
            transform.parent.gameObject.GetComponent<KillerWhale>().hp-= CharacterController.CharacterSingleton.damage;
          
         
        }
        if (hitInfo.gameObject.tag == "SharkBody")
        {
            
          
           
            
        }
    }
}
