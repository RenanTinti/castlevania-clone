using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public GameObject hearth; //500
    public GameObject bigHearth; //290
    public GameObject food; //10
    public GameObject itemKnife; //50
    public GameObject itemAxe; //50
    public GameObject itemHolyWater; //50
    public GameObject itemCross; //50
    int random;
    float hp = 1;
    void Start()
    {

    }
    
    public void TookDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
            Drop();
        }
    }

    public void Drop()
    {
        random = Random.Range(1, 1000);

        if(random >= 1 && random <= 500)
        {
            Instantiate(hearth, gameObject.transform.position, gameObject.transform.rotation);
        }
        if(random >= 501 && random <= 790)
        {
            Instantiate(bigHearth, gameObject.transform.position, gameObject.transform.rotation);
        }
        if(random >= 791 && random <= 800)
        {
            Instantiate(food, gameObject.transform.position, gameObject.transform.rotation);
        }
        if(random >= 801 && random <= 850)
        {
            Instantiate(itemKnife, gameObject.transform.position, gameObject.transform.rotation);
        }
        if(random >= 851 && random <= 900)
        {
            Instantiate(itemAxe, gameObject.transform.position, gameObject.transform.rotation);
        }
        if(random >= 901 && random <= 950)
        {
            Instantiate(itemHolyWater, gameObject.transform.position, gameObject.transform.rotation);
        }
        if(random >= 951 && random <= 1000)
        {
            Instantiate(itemCross, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
