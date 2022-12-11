using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.name == "Enemies")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            print(other.GetComponent<Enemy>().Health);
        }
    }
}
