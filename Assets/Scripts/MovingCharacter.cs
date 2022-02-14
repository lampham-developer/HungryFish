using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
	private Rigidbody2D rigidbody;
	private bool facingRight = true;
	private float movementSpeed = 30f;
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
	private Vector3 zeroVelocity = Vector3.zero;

	private void Awake()
	{

	}

	private void Update()
	{
		Move();
	}


	public void Move()
	{
		Vector2 mousePos = Input.mousePosition;
		Vector3 newVector = mousePos - rigidbody.position;
		Vector3 newVelocity = newVector / newVector.magnitude * movementSpeed;
		//Vector3 targetVelocity = new Vector2(move * 10f, rigidbody.velocity.y);
		
		rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, newVelocity, ref zeroVelocity, movementSmoothing);

		//Quay dau character
		if (newVelocity.x > 0 && !facingRight)
		{
			Flip();
		}
		else if (newVelocity.x < 0 && facingRight)
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