using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Set the values in the inspector
    [SerializeField] private float _speed = 2f;
    private float health = 100;
    public float _within_range;
    public Transform target; // drag and stop player object in the inspector
    public float minWaitTime = 1;
    public float maxWaitTime = 4;

    private Rigidbody rb;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // jump randomly
    IEnumerator JumpLogic()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            Jump();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(JumpLogic());
    }

    // Update is called once per frame
    void Update()
    {
        // Movement();
        ChasePlayer();

        if (Health < 0.5f)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        transform.Translate(new Vector3(0, Random.Range(-1f, 1f), 0.5f) * _speed * Time.deltaTime);
    }

    void ChasePlayer()
    {
        if (target == null)
        {
            return;
        }

        // face the target, freeze on Y axis
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        // get the distance between the player and enemy (this object)
        float dist = Vector3.Distance(target.position, transform.position);

        // check if it is within the range
        if (dist <= _within_range)
        {
            // move to target
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            // else, if it is not in the range, it will not follow player
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
    }
}
