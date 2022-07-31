using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    public GameObject fire;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(fire, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
