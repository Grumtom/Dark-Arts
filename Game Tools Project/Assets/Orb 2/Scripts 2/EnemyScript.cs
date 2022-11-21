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
    public NavMeshAgent agent;
    public GameObject target;
    private Vector3 spawn;
    public string element;
    public string[] strengths;
    public string[] weaknesses;
    public float health = 100f;
    public Image healthbarSocket;
    public Image healthbar;
    public float ditanceToPlayer;
    public float agroRange = 20;

    private void Awake()
    {
        spawn = transform.position; // sets spawn
        healthbarSocket.gameObject.SetActive(false); // turns the healthbar off
        healthbar.gameObject.SetActive(false);
        target = GameObject.Find("Player");
    }

    void Update()
    {
        ditanceToPlayer = (target.transform.position - gameObject.transform.position).sqrMagnitude; // finds range to player
        
        if (ditanceToPlayer <= agroRange)
        {
            agent.SetDestination(target.transform.position); // follows the target
        }
        else
        {
            agent.SetDestination(spawn); // goes to spawn if not in range
        }

        if(health <= 0){Destroy(gameObject);} // dies of death
    }

private void OnCollisionEnter(Collision collision)
{
    print("hyah");
    if (collision.gameObject.tag == "Player")
    {
        collision.gameObject.GetComponent<P2Player>().damage(10); // if it touches the player, they take 10
    }
}

public void takeDamage(float damage, string element)
    {
        health -= damage; // takes damage
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        healthbarSocket.gameObject.SetActive(true); // turns the healthbar on
        healthbar.gameObject.SetActive(true);
        
        health = Mathf.Clamp(health, 0, 100f); // clamps health
        healthbar.fillAmount = health/100; // changes healthbar size
    }
}
