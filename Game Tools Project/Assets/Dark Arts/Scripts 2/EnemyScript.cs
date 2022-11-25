using System;
using System.Collections;
using System.Collections.Generic;
using Orb_2.Scripts_2;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public bool PracticeBot = false;
    public float navmeshSpeed = 2.5f;
    public NavMeshAgent agent;
    public GameObject target;
    private Vector3 spawn;
    public string element;
    public string[] strengths;
    public string[] weaknesses;
    public float health = 100f;
    public Image healthbarSocket;
    public Image healthbar;
    public float distanceToPlayer;
    public float agroRange = 20;
    public float speedDebuffTimer = 0;

    [Header("Attack 1")] 
    public float baseAttackRange = 2;
    public float baseDamage = 10;
    public GameObject baseAttackZone;
    public float baseAttackTime;
    public float baseAttackTimer = 0;
    private void Awake()
    {
        spawn = transform.position; // sets spawn
        healthbarSocket.gameObject.SetActive(false); // turns the healthbar off
        healthbar.gameObject.SetActive(false);
        target = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (!PracticeBot)
        {
            distanceToPlayer =
                (target.transform.position - gameObject.transform.position).sqrMagnitude; // finds range to player

            if (distanceToPlayer <= agroRange)
            {
                agent.SetDestination(target.transform.position); // follows the target
            } // target following
            else
            {
                agent.SetDestination(spawn); // goes to spawn if not in range
            }


            baseAttackTimer--; // ticks down the time spent attacking
            if (baseAttackTimer <= 0)
            {
                baseAttackZone.SetActive(false); // turns off the attack
                agent.isStopped = false;
            }

            if (distanceToPlayer <= baseAttackRange && baseAttackTimer < 0) // attacks
            {
                baseAttackZone.SetActive(true);
                baseAttackTimer = baseAttackTime; // sets the attack zone active then starts the timer to deactivate it
                agent.isStopped = true;
            }

            if (health <= 0 && !PracticeBot)
            {
                Destroy(gameObject);
            } // dies of death

            speedDebuffTimer--;
            if (speedDebuffTimer <= 0)
            {
                agent.speed = navmeshSpeed; // speeds back up after blizzard slows
            }
        }
    }

    public void takeDamage(float damage, string element)
    {
        health -= damage; // takes damage
        if (health <= 0 )
        {
            if (!PracticeBot)
            {
                Destroy(gameObject);
            }
            else
            {
                health = 100;
            }
   
        }
        
        
        healthbarSocket.gameObject.SetActive(true); // turns the healthbar on
        healthbar.gameObject.SetActive(true);
        
        health = Mathf.Clamp(health, 0, 100f); // clamps health
        healthbar.fillAmount = health/100; // changes healthbar size
    }
}
