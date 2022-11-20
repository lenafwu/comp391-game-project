using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _item;
    // Start is called before the first frame update
    void Start()
    {
        // Spawn an item for 5 times
        // ! Assign the game object in Unity !
        for (int i = 0; i < 50; i++)
        {
            // SpawnItem(3 * i + 3 * Mathf.Sin(Random.Range(0, Mathf.PI / 2)), 3 * Mathf.Sin(Random.Range(0, Mathf.PI / 2)), -1.49f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Clone item in a random place
    void SpawnItem(float x, float y, float z)
    {
        // Instantiate(_item, new Vector3(Random.Range(20f, 2.5f), Random.Range(0, 5f), Random.Range(-2f, 0)), Quaternion.identity);
        GameObject go = Instantiate(_item, new Vector3(x, y, z), Quaternion.identity);
        go.transform.localScale =
            new Vector3(1 + 3 * Mathf.Sin(Random.Range(0, Mathf.PI / 2)), 1.0f, 1.0f);

    }
}
