using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _item;
    [SerializeField] List<GameObject> _itemList;
    [SerializeField] private int _spawnNumber;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn an item
        // ! Assign the game object in Unity !
        for (int i = 0; i < _spawnNumber; i++)
        {
            //  SpawnItem(3 * i + 3 * Mathf.Sin(Random.Range(0, Mathf.PI / 2)), 3 * Mathf.Sin(Random.Range(0, Mathf.PI / 2)), -1.49f);
            SpawnItem(_itemList[Random.Range(0, _itemList.Count)], Random.Range(0, 10), 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Clone item in a random place
    void SpawnItem(GameObject item, float x, float y, float z)
    {
        GameObject go = Instantiate(item, new Vector3(x, y, z), Quaternion.identity);

    }
}
