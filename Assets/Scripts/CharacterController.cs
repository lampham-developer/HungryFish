using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour
{
    public static CharacterController CharacterSingleton;
    private bool facingRight = true;
    [SerializeField]
    GameObject explosion;
    public float maxMoveSpeed = 5;
    public float smoothTime = 0.3f;
    public float minDistance = 0;
    public Camera camera;
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
    AudioSource audio;

    private Vector3 currentScale;
    private float currentCameraScale;
    
    public float damage = 4f;

    private float decreaseHeathThreshold = 5f;
    private float gameTime = 0f;
    private float decreaseHeathMultiple = 1;

    bool isGameEnded = false;

    private void Awake()
    {
        CharacterSingleton = this;
        // set default value for slider
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthFill.color = healthGradient.Evaluate(1f);

        staminaSlider.maxValue = cooldown;
        staminaSlider.value = currentCooldown;
        audio = GetComponent<AudioSource>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        currentScale = transform.localScale;
        currentCameraScale = camera.orthographicSize;

    }

    private void Update()
    {
        if (isGameEnded)
            return;
        //update game time
        gameTime += Time.deltaTime;

        //Add more stamina each fram
        if (currentCooldown < cooldown)
        {
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
		if(Input.GetKeyDown	(KeyCode.Space)){
			useSkill2();
		}

    }

    public static CharacterController getInstance()
    {
        return CharacterSingleton;
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

    private void setMaxHealthBar()
    {
        healthSlider.maxValue = maxHealth;
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
         rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0; 
        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        if (facingRight)
        {
            theScale.x = Mathf.Abs(theScale.x);
        }else
        {
            theScale.x = Mathf.Abs(theScale.x) * -1;
        }

        transform.localScale = theScale;
    }

    private void useSkill()
    {
        if (currentCooldown >= cooldown)
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
            rigidbody.AddForce(destination.normalized*3f,ForceMode2D.Impulse);
           // transform.position = Vector2.LerpUnclamped(transform.position, destination, 0.1f);
            currentCooldown = 0;
        }

    }

    private void updateHealthBar()
    {
        healthSlider.value = currentHealth;
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void playAudio()
    {
        audio.Play();
    }

    private void updateStaminaBar()
    {
        staminaSlider.value = currentCooldown;
    }

    public void decreaseHealth(float time)
    {
        if (currentHealth > 0)
        {
            float amount = time * gameTime / decreaseHeathThreshold;

            currentHealth -= amount;
            updateHealthBar();
        }

        if (currentHealth <= 0)
        {
            isGameEnded = true;
            GameController.GameControllerSingleton.endGame();

        }
    }

    public void useSkill2()
    {
        if (currentCooldown >= cooldown)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Creep");
            foreach (var obj in objs)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) < 10)
                {
                    Destroy(obj);
                    increaseHealth(5f);
                    GameController.GameControllerSingleton.scoreUp(5);
                    GameObject explo = Instantiate<GameObject>(explosion, obj.transform.position, Quaternion.identity);

                }
            }
            currentCooldown = 0;
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

    public void upLevelShark(float mvSpeed, float health, float size)
    {
        maxMoveSpeed = mvSpeed;
        maxHealth = health;

        setMaxHealthBar();
        changeSharkSize(size);
    }

    private void changeSharkSize(float newSize)
    {
        currentScale *= newSize;
        currentCameraScale *= newSize;

        transform.localScale = currentScale;
        camera.orthographicSize = currentCameraScale;
    }
}