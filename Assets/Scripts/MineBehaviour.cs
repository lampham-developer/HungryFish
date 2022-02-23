using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    float damage =20f;
    
    
    [SerializeField]
    GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
      void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "SharkBody")
        {
            CharacterController.CharacterSingleton.decreaseHealth(damage);
            GameObject shark = GameObject.FindGameObjectWithTag("AShark");
            GameObject obj = Instantiate<GameObject>(explosion, gameObject.transform.position, Quaternion.identity);
            shark.transform.localPosition = Vector3.Lerp (shark.transform.localPosition, -transform.localPosition, 0.1f);
            
            Destroy(gameObject);
        }
       
    }
}
