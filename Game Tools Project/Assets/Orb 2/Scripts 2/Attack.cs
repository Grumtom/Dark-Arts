using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Attack : MonoBehaviour
{
    public bool hasLife;
    public bool projectile;
    public string element;
    public bool continuous;
    public bool spin;
   // public bool lazer;
    
    public float damageTick;
    public float life;
    public float damage;

    // Update is called once per frame
    void Update()
    {
        if (hasLife)
        {
            life--;
            if(life<=0){Destroy(gameObject);}
        }
        Random rnd = new Random();
        if(spin){gameObject.transform.Rotate(rnd.Next(-10,10),rnd.Next(-10,10),rnd.Next(-10,10));}
    //    if (lazer) {LazerLength();}
    }

    void LazerLength()
    {
        RaycastHit hit;
        Ray lazerRay = new Ray(transform.position + new Vector3(0, 1, 0), transform.forward);
        if (Physics.Raycast(lazerRay, out hit)) {float lazerLength = hit.distance;}
        transform.localScale = new Vector3(1, 1, hit.distance);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (projectile && collision.gameObject.tag != "Player" && collision.gameObject.tag != "playerAttack")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().takeDamage(damage, element);
            if(projectile){Destroy(gameObject);}
        }
        if (projectile && collision.gameObject.tag != "Player" && collision.gameObject.tag != "playerAttack")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (continuous)
            {
                damageTick--;
                if (damageTick <= 0)
                {
                    collision.gameObject.GetComponent<EnemyScript>().takeDamage(damage, element);
                }
            }
        }
    }
}
