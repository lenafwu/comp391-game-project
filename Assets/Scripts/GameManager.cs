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
        for (int i = 0; i < 5; i++)
        {
            SpawnItem();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Clone item in a random place
    void SpawnItem()
    {
        Instantiate(_item, new Vector3(Random.Range(6.5f, 0), Random.Range(6.5f, 0), Random.Range(6.5f, 0)), Quaternion.identity);
    }
}
