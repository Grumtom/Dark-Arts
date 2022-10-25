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
    public Image healthbar;

    private void Awake()
    {
        spawn = transform.position; // sets spawn
    }

    void Update()
    {
        agent.SetDestination(target.transform.position); // follows the target
        if(health <= 0){Destroy(gameObject);} // dies of death
    }
/*
    private void OnTriggerEnter(Collider other) // if its shot
    {
    if (other.gameObject.tag == element)
   {
       transform.position = spawn; // respawns
       print("dead");
   }
    }
*/

private void OnCollisionEnter(Collision collision)
{
    print("hyah");
    if (collision.gameObject.tag == "Player")
    {
        collision.gameObject.GetComponent<P2Player>().damage(10);
    }
}

public void takeDamage(float damage, string element)
    {
        health -= damage; // takes damage
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        health = Mathf.Clamp(health, 0, 100f); // clamps health
        healthbar.fillAmount = health/100; // changes healthbar size
    }
}
