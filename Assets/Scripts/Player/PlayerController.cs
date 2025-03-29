using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float maxSpeed = 6f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    [HideInInspector]
    public bool lookingRight = true;
    bool doubleJump = false;
    public GameObject Boost;

    private Animator cloudanim;
    public GameObject Cloud;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isGrounded = false;
    private float horizontalInput = 0f;
    [SerializeField] private InputManager inputManager;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //cloudanim = GetComponent<Animator>();

        Cloud = GameObject.Find("Cloud");
        //cloudanim = GameObject.Find("Cloud(Clone)").GetComponent<Animator>();

        // Ensure InputManager reference is valid
        if (inputManager == null)
        {
            inputManager = FindFirstObjectByType<InputManager>();
            if (inputManager == null)
            {
                Debug.LogError("InputManager not found! Please add an InputManager to the scene.");
                return;
            }
        }

        // Subscribe to input events
        inputManager.OnJump.AddListener(PerformJump);
        inputManager.OnMoveHorizontal.AddListener(HandleMovement);
        inputManager.OnDownThrust.AddListener(PerformDownThrust);
    }

    void OnDestroy()
    {
        // Unsubscribe from events when destroyed
        if (inputManager != null)
        {
            inputManager.OnJump.RemoveListener(PerformJump);
            inputManager.OnMoveHorizontal.RemoveListener(HandleMovement);
            inputManager.OnDownThrust.RemoveListener(PerformDownThrust);
        }
    }

    void HandleMovement(float horizontalValue)
    {
        horizontalInput = horizontalValue;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.relativeVelocity.magnitude > 20)
        {
            InstantiateCloud();
        }
    }

    // Helper method to instantiate cloud
    public void InstantiateCloud()
    {
        Boost = Instantiate(Resources.Load("Prefab/Cloud"), transform.position, transform.rotation) as GameObject;
    }

    void PerformJump()
    {
        if (isGrounded || !doubleJump)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !isGrounded)
            {
                doubleJump = true;
                InstantiateCloud();
            }
        }
    }

    void PerformDownThrust()
    {
        if (!isGrounded)
        {
            rb2d.AddForce(new Vector2(0, -jumpForce));
            InstantiateCloud();
            GameManager.instance.SetDownThrust(true);
        }
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            doubleJump = false;
            GameManager.instance.SetDownThrust(false);
        }

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));


        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        rb2d.linearVelocity = new Vector2(horizontalInput * maxSpeed, rb2d.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, whatIsGround);

        anim.SetBool("IsGrounded", isGrounded);

        if ((horizontalInput > 0 && !lookingRight) || (horizontalInput < 0 && lookingRight))
            Flip();

        anim.SetFloat("vSpeed", rb2d.linearVelocity.y);
    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}