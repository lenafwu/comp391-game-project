using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < 5f)
        {
            moveSpeed = 1f;
        }
        if (this.transform.position.x > 10f)
        {
            moveSpeed = -1f;
        }
        transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime);
        // print(this.transform.position.x);
    }
}
