using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Transform playerPosition;
    public float hp;
    public float speed;
    public float damage = 5;
    public bool facingRight;

    void Start()
    {

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        if(playerPosition.gameObject != null && playerPosition.transform.position.x > this.gameObject.transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            facingRight = true;
        }

        else if(playerPosition.gameObject != null && playerPosition.transform.position.x < this.gameObject.transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x *= 1;
            transform.localScale = scale;
            facingRight = false;
        }
    }

    void Update()
    {
        if(facingRight == true)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        }

        else
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TookDamage(damage);
            other.gameObject.GetComponent<Player>().DamageHealthBar(damage);
        }
    }

    public void TookDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}