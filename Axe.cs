using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
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
        switch(other.tag)
        {
            case "Zombie":
                other.gameObject.GetComponent<Zombie>().TookDamage(damage);
            break;
            case "Skeleton":
                other.gameObject.GetComponent<Skeleton>().TookDamage(damage);
            break;
            case "Bat":
                other.gameObject.GetComponent<Bat>().TookDamage(damage);
            break;
            case "Bone":
                Destroy(other.gameObject);
            break;
            case "Candle":
                other.gameObject.GetComponent<Candle>().TookDamage(damage);
                Destroy(other.gameObject);
            break;
        }
    }
}
