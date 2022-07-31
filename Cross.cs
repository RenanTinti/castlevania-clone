using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    public float damage = 5;
    public float speed = 20;
    private int cont = 0;

    void Start()
    {
        Destroy(gameObject, 4);
    }


    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if(cont >= 2)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "CrossWall":
                cont++;
                speed = -speed;
            break;
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
