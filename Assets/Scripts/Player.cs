using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    // Exposed to Unity
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float playerSpeed = 5f;
    public AudioClip attackSound;
    // [SerializeField] private float rotateSpeed = 720;
    [SerializeField] private float jumpSpeed = 7f;
    public HealthBar healthBar;


    // TODO smt to do with player health
    public float maxHealth = 100f;
    private float playerHealth = 100f;

    public float Health
    {
        get
        {
            return playerHealth;
        }
        set
        {
            playerHealth = value;
        }
    }

    public GameObject _enemy;

    // Invisible in Unity
    private Rigidbody rb;
    private bool jumpKeyPressed;
    private bool isGrounded;
    private bool isWalking;

    private bool isAttacking = false;
    private float timeToAttack = 0.3f;
    private float timer = 0f;
    private GameObject attackArea;


    private float horizontalInput;
    private float verticalInput;

    private Vector3 offset;
    private int score = 0;

    private AudioSource source;

    private Vector3 movementDirection;

    private UIManager _UIManager;

    private Animator anim;

    // TODO some logic here
    void TakeDamage()
    {
        playerHealth -= 10;
        healthBar.UpdateHealthBar();
    }

    void AddHealth()
    {
        playerHealth += 5;
        healthBar.UpdateHealthBar();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        source = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        attackArea = transform.GetChild(0).gameObject;
    }

    // Check collision
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // teleport
            if (contact.otherCollider.name == "TeleportDoor")
            {
                transform.position = GameObject.Find("TeleportDestination").transform.position;
            }

            // check if grounded
            if (contact.otherCollider.transform.parent.gameObject.name == "Level")
            {
                isGrounded = true;
            }

            // check if collided with enemy and take damage
            if (contact.otherCollider.transform.parent.gameObject.name == "Enemies")
            {
                TakeDamage();
                anim.SetTrigger("GettingHit");
            }
        }

    }



    // Update is called once per frame
    void Update()
    {
        // press F to attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
            source.PlayOneShot(attackSound);
        }

        if (isAttacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                isAttacking = false;
                attackArea.SetActive(isAttacking);
            }
        }

        // Player dies when fall off the platforms
        if (transform.position.y < -1)
        {
            // EditorApplication.isPlaying = false;
        }

        // Check if player is jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpKeyPressed = true;
        }


        // Get horizontal and vertical input to make the player move
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //    anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        //    anim.SetFloat("vertical", Input.GetAxis("Horizontal"));


        // Rotate player
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }

    }

    // FixedUpdate is called one every time physics update
    private void FixedUpdate()
    {
        // Put this velocity at the top line, so the player can move while jumping
        rb.velocity = new Vector3(horizontalInput * playerSpeed, rb.velocity.y, verticalInput * playerSpeed);

        if (isGrounded)
        {
            anim.SetFloat("walking", rb.velocity.sqrMagnitude);
        }
        else
        {
            anim.SetFloat("walking", 0);
        }
        // add a force up when jumping
        if (jumpKeyPressed)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);

            isGrounded = false;
            jumpKeyPressed = false;

            source.Play();

        }

        // TODO doesn't work anymore!!!
        // Preventing Player from jumping in the air
        // 1. in Unity, add a layer to Player
        // 2. in this script, add a Layermask field called playerMask
        // 3. in Unity, in the field 'Player Mask', check Everything except for Player (because the player always colliding with itself)
        // 4. Physics.OverlapSphere returns an array of the colliders
        // 5. if length == 0, the player isn't colliding with anything, i.e. in the air
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
    }

    // picking up the coins
    // in coin's collider, check 'is Trigger'
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // the layer where the coins are
        {
            Destroy(other.gameObject);
            score++;
            _UIManager.updateScore(score);
        }

        if (other.gameObject.layer == 9) // food
        {
            if (Health < 99f)
            {
                Destroy(other.gameObject);
                AddHealth();
                anim.SetTrigger("Eating");
            }

        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        isAttacking = true;
        attackArea.SetActive(isAttacking);
    }
}
