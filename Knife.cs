using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float damage;
    public float speed;

    void Start()
    {
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Zombie":
                other.gameObject.GetComponent<Zombie>().TookDamage(damage);
                Destroy(this.gameObject);
            break;
            case "Skeleton":
                other.gameObject.GetComponent<Skeleton>().TookDamage(damage);
                Destroy(this.gameObject);
            break;
            case "Bat":
                other.gameObject.GetComponent<Bat>().TookDamage(damage);
                Destroy(this.gameObject);
            break;
            case "Bone":
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            break;
            case "Candle":
                other.gameObject.GetComponent<Candle>().TookDamage(damage);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            break;
        }
    }
}
