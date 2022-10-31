using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Exposed to Unity
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float playerSpeed = 5f;

    private int playerHealth = 100;
    public int PlayerHealth
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
    private float horizontalInput;
    private float verticalInput;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = this.transform.position - _enemy.transform.position;
        if (offset.sqrMagnitude < 0.7f)
        {
            print("Enemy too close!");

        }

        // Check if key space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }

        // Get horizontal and vertical input to make the player move

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

    }

    // FixedUpdate is called one every time physics update
    private void FixedUpdate()
    {
        // Put this velocity at the top line, so the player can move while jumping
        rb.velocity = new Vector3(horizontalInput * playerSpeed, rb.velocity.y, verticalInput);

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

        // add a force up when jump key is pressed
        if (jumpKeyPressed)
        {
            rb.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }
    }

    // picking up the coins
    // in coin's collider, check 'is Trigger'
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // the layer where the coins are
        {
            Destroy(other.gameObject);
        }
    }
}
