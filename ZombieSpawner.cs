using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject enemy;
    private float spawnRate;
    float nextSpawn = 0;

    void Update()
    {
        spawnRate = Random.Range(5, 15);
        
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            Instantiate(enemy, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
