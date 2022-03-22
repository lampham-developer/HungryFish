using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSmallFish : MonoBehaviour
{
    // Start is called before the first frame update
    protected Timer timer;
     protected bool facingRight ;
    protected Vector2 direction;

    protected abstract int increaseScore { get; }
    public virtual  void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
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
    public abstract void MoveSlowly();
    public abstract void Run();
    public float GetDistanceWithPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("AShark");
        return Vector3.Distance(player.transform.position, gameObject.transform.position);


    }
    

}
