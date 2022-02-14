using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour
{
	private bool facingRight = true;

	public float maxMoveSpeed = 5;
	public float smoothTime = 0.3f;
	public float minDistance = 0;
	Vector2 currentVelocity;

	private void Awake()
	{

	}

	private void FixedUpdate()
	{
		Move();
	}


	public void Move()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Offsets the target position so that the object keeps its distance.
		mousePosition += ((Vector2)transform.position - mousePosition).normalized * minDistance;
		transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, maxMoveSpeed);

        //flip the character
        if (currentVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (currentVelocity.x < 0 && facingRight)
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
}