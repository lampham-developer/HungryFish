using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour
{
	public static CharacterController CharacterSingleton;
	private bool facingRight = true;

	public float maxMoveSpeed = 5;
	public float smoothTime = 0.3f;
	public float minDistance = 0;
	Vector2 currentVelocity;
	private float force = 50;

	private float cooldown = 2;
	private float currentCooldown = 0;

	public Rigidbody2D rigidbody;

	private float maxHealth = 100;
	private float currentHealth = 100;

	public Slider healthSlider;
	public Slider staminaSlider;
	public Image healthFill;
	public Gradient healthGradient;


	private void Awake()
	{
		CharacterSingleton = this;
		// set default value for slider
		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth;
		healthFill.color = healthGradient.Evaluate(1f);

		staminaSlider.maxValue = cooldown;
		staminaSlider.value = currentCooldown;
	}

	private void Update()
	{
		//Add more stamina each fram
		if (currentCooldown < cooldown) { 
			currentCooldown += Time.deltaTime; 
			updateStaminaBar();
		}

		//Decrease shark's health every frame
		decreaseHealth(Time.deltaTime);

		//check moving & skill
		Move();

		if (Input.GetMouseButtonDown(0))
		{
			useSkill();
		}
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

	private void useSkill()
    {
		if(currentCooldown >= cooldown)
        {
			Vector2 destination;
			if (facingRight)
			{
				destination = new Vector2(transform.position.x + 50, transform.position.y);

			}
			else
			{
				destination = new Vector2(transform.position.x - 50, transform.position.y);
			}
			transform.position = Vector2.LerpUnclamped(transform.position, destination, 0.1f);
			currentCooldown = 0;
		}
		
    }

	private void updateHealthBar()
    {
		healthSlider.value = currentHealth;
		healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
	}

	private void updateStaminaBar()
	{
		staminaSlider.value = currentCooldown;
	}

	public void decreaseHealth(float amount)
    {
		if(currentHealth > 0)
        {
			currentHealth -= amount;
			updateHealthBar();
		}

		if(currentHealth <= 0)
        {
			GameController.GameControllerSingleton.endGame();

		}
    }

	public void increaseHealth(float amount)
	{
		currentHealth += amount;

		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}

		updateHealthBar();

	}
}