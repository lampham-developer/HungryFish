using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdFish : AbstractSmallFish
{
    float normalSpeed = 1f;
    float runSpeed = 3f;
    int active = 0;
    Vector2 groupDirection = new Vector2(1, 0);
    //private float increaseScore=5f;
    private float increaseHealth=5f;

    protected override int increaseScore => 5;

    public override void MoveSlowly()
    {
        if (timer.Finished)
        {
            direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            timer.Run();

        }
        GameObject player = GameObject.FindGameObjectWithTag("AShark");
        gameObject.transform.position = gameObject.transform.position + new Vector3(direction.x * normalSpeed * Time.deltaTime, direction.y * normalSpeed * Time.deltaTime, 0);
    }

    public void MoveSlowly(Vector2 d)
    {

        GameObject player = GameObject.FindGameObjectWithTag("AShark");
        gameObject.transform.position = gameObject.transform.position + new Vector3(d.x * normalSpeed * Time.deltaTime, d.y * normalSpeed * Time.deltaTime, 0);

    }

    public override void Run()
    {
        GameObject player = GameObject.FindGameObjectWithTag("AShark");
        float step = runSpeed * Time.deltaTime;
        direction.x = transform.position.x - player.transform.position.x;
        Vector2 point = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, point, -1 * step);
        active = 1;
    }

    public override void Start()
    {
        facingRight = false;
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 5f;
        timer.Run();
        direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }



    public override void Update()
    {
        if (active == 0)
        {
            if (base.GetDistanceWithPlayer() < 6)
            {
                Run();

            }
            else
            {
                MoveSlowly(groupDirection);
            }
            if (direction.x > 0 && !facingRight)
            {
                base.Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                base.Flip();
            }

        }
        else{
            if (base.GetDistanceWithPlayer() < 6)
            {
                Run();

            }
            else
            {
                MoveSlowly();
            }
            if (direction.x > 0 && !facingRight)
            {
                base.Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                base.Flip();
            }

        }
        
    }

    // Start is called before the first frame update
     void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "SharkMounth")
        {
            CharacterController.CharacterSingleton.increaseHealth(increaseHealth);
            GameController.GameControllerSingleton.scoreUp(increaseScore);
            GameController.GameControllerSingleton.removeFish();
            CharacterController.CharacterSingleton.playAudio();
            Destroy(gameObject);
        }
    }
}
