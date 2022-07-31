using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public float damage;
    
    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 30));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TookDamage(damage);
            other.gameObject.GetComponent<Player>().DamageHealthBar(damage);
        }
    }
}
