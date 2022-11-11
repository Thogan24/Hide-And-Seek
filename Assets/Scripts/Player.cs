using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController controller;
    public Gun Gun;
    public GameObject GunObject;

    [Header("PlayerStats")]
    public int playerHealth = 100;
    public int playerDamage = 1;
    public GameObject Target;
    public GameObject particleEffects;
    public GameObject player;


    [Header("PlayerMovement")]
    public float speed = 30f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float moveSpeed;
    public float wallrunSpeed;
    public float coyoteTimer;
    public bool jumpedOnce;
    public float jumpedOnceTimer = 0f;
    public bool jumpedTwice;

    Vector3 velocity;
    public bool isGrounded;

    public MovementState state;

    public enum MovementState
    {
        walking,
        wallrunning
    }

    public bool wallrunning;



    private void Start()
    {
        Gun = GunObject.GetComponent<Gun>();
    }
    void Update()
    {
        Debug.Log(coyoteTimer);
        Debug.Log("Jumped Once: " + jumpedOnce);
        Debug.Log("Jumped Twice: " + jumpedTwice);
        Debug.Log("isGrounded: " + isGrounded);
        //Creates an invisible sphere below the player in which checks whats below it.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Should be 0 but -2 works better
        }
        if (isGrounded)
        {
            if (jumpedOnceTimer <= 0)
            {
                jumpedOnce = false;
            }
            jumpedTwice = false;
            coyoteTimer = 0.5f;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        // Movement

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (move.magnitude > 0)
        {
            Gun.movingAccuracy = (move.magnitude + 1) * 10;
        }
        else
        {
            Gun.movingAccuracy = 1;
        }

        controller.Move(move * speed * Time.deltaTime);

        

        if(Input.GetButtonDown("Jump") && jumpedOnce == true && jumpedTwice == false)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpedTwice = true;
        }

        if (!isGrounded && coyoteTimer < 0)
        {
            jumpedOnce = true;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimer >= 0) && jumpedOnce == false)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpedOnce = true;
            jumpedOnceTimer = 0.2f;
            
            //GetComponent<Rigidbody>().AddForce(player.eulerAngles.x);
        }

        jumpedOnceTimer -= Time.deltaTime;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); // We multiply by 2 cause the formula is delta y = 1/2g * t^2

        

        /*if (transform.GetComponent<Collider>().bounds.Intersects(Target.GetComponent<Collider>().bounds))
        {
            //particleEffects.SetActive(true);
            DamagePlayer();
        }
        else
        {
            //particleEffects.SetActive(false);
        }
        if (transform.GetChild(3).GetComponent<Collider>().bounds.Intersects(Target.GetComponent<Collider>().bounds))
        {
            particleEffects.SetActive(true);
        }
        else
        {
            particleEffects.SetActive(false);
        }*/
    }

    void StateHandler()
    {
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            moveSpeed = wallrunSpeed;
        }
    }

    void DamagePlayer()
    {
        Debug.Log(playerHealth);
        playerHealth--;
    }


}