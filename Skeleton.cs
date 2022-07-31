using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed;
    public float damage;
    public float hp;
    public float changeSpeed;
    public float atkRate;
    float nextAtk = 0;
    float nextChange = 0;
    public bool facingRight;
    public Rigidbody2D bone;
    private Transform playerPosition;
    Vector3 scale;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(playerPosition.position.x > this.gameObject.transform.position.x && !facingRight)
        {
            Flip();
            facingRight = true;
        }
        else if(playerPosition.position.x < this.gameObject.transform.position.x && facingRight)
        {
            Flip();
            facingRight = false;
        }

        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(Time.time > nextChange)
        {
            nextChange = Time.time + changeSpeed;
            speed *= -1;
        }

        atkRate = Random.Range(2, 5);

        if(Time.time > nextAtk && Vector2.Distance(transform.position, playerPosition.position) < 10)
        {
            nextAtk = Time.time + atkRate;
            Rigidbody2D tempBone = Instantiate(bone, this.gameObject.transform.position, this.gameObject.transform.rotation);
            if(facingRight)
            {
                tempBone.AddForce(new Vector2(5, 15), ForceMode2D.Impulse);
            }
            else
            {
                tempBone.AddForce(new Vector2(-5, 15), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
