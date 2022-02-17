using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFishMover : MonoBehaviour
{
    Timer timer;
    float normalSpeed = 1f;
    float runSpeed = 3f;
    private bool facingRight = false;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 5f;
        timer.Run();
        direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }

    // Update is called once per frame
    void Update()
    {

        if (GetDistanceWithPlayer() < 5)
        {
            Run();

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
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void MoveSlowly()
    {
        if (timer.Finished)
        {
            direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            timer.Run();

        }
        GameObject player = GameObject.FindGameObjectWithTag("AShark");



        gameObject.transform.position = gameObject.transform.position + new Vector3(direction.x * normalSpeed * Time.deltaTime, direction.y * normalSpeed * Time.deltaTime, 0);



    }
    void Run()
    {
        GameObject player = GameObject.FindGameObjectWithTag("AShark");
        float step = runSpeed * Time.deltaTime;
        direction.x =transform.position.x - player.transform.position.x;
        Vector2 point = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, point, -1 * step);


    }
    float GetDistanceWithPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("AShark");
        return Vector3.Distance(player.transform.position, gameObject.transform.position);


    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "SharkMounth")
        {
            Destroy(gameObject);
        }
    }
}
