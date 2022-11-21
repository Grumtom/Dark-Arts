using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Attack : MonoBehaviour
{
    public bool hasLife;
    public bool projectile;
    public string element;
    public bool continuous;
    public bool spin;
    public bool waterBolt;
    public bool waterSpin;

    public float waterSpeed;
    public float damageTick;
    public float life;
    public float damage;

    // Update is called once per frame
    void Update()
    {
        Random rnd = new Random();
        if (hasLife)
        {
            life--;
            if(life<=0){Destroy(gameObject);}
        } // lifespan tickdown
        if(spin){gameObject.transform.Rotate(rnd.Next(-10,10),rnd.Next(-10,10),rnd.Next(-10,10));} // rock's shaky spin
        if(waterBolt){goToClosestEnemy();} // waterbolt tracking
        if(waterSpin)transform.Rotate(0,2,0); // waterbolt spin
    }

    private void OnCollisionStay(Collision collision)
    {
        if (projectile && collision.gameObject.tag != "Player" && collision.gameObject.tag != "playerAttack")
        {
            Destroy(gameObject);
        }
    } // destroy projectile when hit wall
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
    } // hurt enemy and kill projectile
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
    } // continuous damage
    void goToClosestEnemy()
    {
        float distanceToEnemy = 100000;
        EnemyScript closestEnemy = null; // the closest enemy does not exist yet
        EnemyScript[] allEnemies = GameObject.FindObjectsOfType<EnemyScript>();
        
        if (allEnemies.Length != 0) // if there are enemies
        {
            foreach (EnemyScript thisone in allEnemies) // for each enemy
            {
                float distance = (thisone.transform.position - this.transform.position).sqrMagnitude; // for each enemy check if its closer, 
                if (distance < distanceToEnemy)
                {
                    distanceToEnemy = distance;
                    closestEnemy = thisone; // sets the new closest enemy to this one if this one is closer
                }
            }

            Debug.DrawLine(gameObject.transform.position,closestEnemy.transform.position); //funny lines go brr
            gameObject.transform.position =
                Vector3.MoveTowards(gameObject.transform.position, closestEnemy.transform.position, waterSpeed);
        }
    }
}
