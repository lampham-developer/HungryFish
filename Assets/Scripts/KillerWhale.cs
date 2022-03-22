using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerWhale : MonoBehaviour
{

    float normalSpeed = 1f;
    float runSpeed = 3f;
    float increaseHealth = 20f;
    int increaseScore = 20;
    float increaseExp = 20f;
    public float hp=50f;
    public float damage = 10f;
    Vector2 direction;
bool facingRight ;
    Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
     
        facingRight=true;
        timer.Duration = 5f;
        timer.Run();
        direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp<=0) {

            CharacterController.CharacterSingleton.increaseHealth(increaseHealth);
            GameController.GameControllerSingleton.scoreUp(increaseScore);
            GameController.GameControllerSingleton.removeFish();
            Destroy(gameObject);
            
        }
         if (GetDistanceWithPlayer() < 7)
        {
            chase();

        }
        else
        {
            MoveSlowly();
        }
            if (direction.x > 0 && !facingRight)
        {
           Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
           Flip();
        }
        
    }
    public float GetDistanceWithPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("AShark");
        return Vector3.Distance(player.transform.position, gameObject.transform.position);


    }
     public void MoveSlowly()
    {
        if (timer.Finished)
        {
            direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            timer.Run();

        }
        GameObject player = GameObject.FindGameObjectWithTag("AShark");


        float step =normalSpeed * Time.deltaTime;
       transform.position = Vector2.MoveTowards(transform.position, direction,step);



    }

    public void chase(){
        GameObject player = GameObject.FindGameObjectWithTag("AShark");
        float step = runSpeed * Time.deltaTime;
        direction.x =-transform.position.x + player.transform.position.x;
        Vector2 point = new Vector2(player.transform.position.x, player.transform.position.y);
        //direction=new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, point,step);
    }
     public void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
      

    
}
