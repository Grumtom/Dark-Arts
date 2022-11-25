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
    public bool rock;
    public bool waterBolt;
    public bool waterSpin;
    public bool spray;
    public bool blizzard;

    public float blizzardDebuffTime = 100;
    public float waterSpeed;
    public float damageTick;
    private float damageTimer = 0;
    public float life;
    public float damage;

    public GameObject rockblasts = null;

    // Update is called once per frame
    void FixedUpdate()
    {
        Random rnd = new Random();
        if (hasLife)
        {
            life--;
            if(life<=0){Destroy(gameObject);}
        } // lifespan tickdown
        if(rock){gameObject.transform.Rotate(rnd.Next(-10,10),rnd.Next(-10,10),rnd.Next(-10,10));} // rock's shaky spin
        if(waterBolt){goToClosestEnemy();} // waterbolt tracking
        if(waterSpin)transform.Rotate(0,8,0); // waterbolt spin
        if(spray){gameObject.transform.localRotation = Quaternion.Euler(0,transform.rotation.y,0);} // holds the spray still
    }

   /* private void OnCollisionStay(Collision collision)
    {
        if (projectile && collision.gameObject.tag != "Player" && collision.gameObject.tag != "playerAttack")
        {
            if(rock)
            {
                GameObject rockblast = Instantiate(rockblasts, transform.position, transform.rotation); // spawns the rock explosion attacks
                rockblast.transform.SetParent(gameObject.transform);
            }
            Destroy(gameObject);
        }
    }*/ // destroy projectile when hit wall
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().takeDamage(damage, element); // damages enemies if projectile
            if(projectile){Destroy(gameObject);}
        }
        if (projectile && collision.gameObject.tag != "Player" && collision.gameObject.tag != "playerAttack")
        {
            if(rock)
            {
                GameObject rockblast = Instantiate(rockblasts, transform.position, transform.rotation); // spawns the rock explosion attacks
                print("boom");
            }
            Destroy(gameObject);
        }
    } // hurt enemy and kill projectile
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (blizzard)
            {
            
                collision.GetComponent<NavMeshAgent>().speed = 1f;
                collision.gameObject.GetComponent<EnemyScript>().speedDebuffTimer = blizzardDebuffTime;
            }     // slows the enemy if blizzard
            if (continuous)
            {
                damageTimer--;
                if (damageTimer <= 0)
                {
                    collision.gameObject.GetComponent<EnemyScript>().takeDamage(damage, element); // causes damage continuously
                    damageTimer = damageTick;
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
