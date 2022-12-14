using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    public float Distance;

    public bool isTriggered;

    public NavMeshAgent _agent;

    private int health = 5;

    public Slider healthBar;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }


    private void Update()
    {
        Distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if (Distance <= 40)
        {
            isTriggered = true;
        }
        if (Distance > 40)
        {
            isTriggered = false;
        }

        if (isTriggered)
        {
            _agent.isStopped = false;
            _agent.SetDestination(Player.transform.position);
        }
        if (!isTriggered)
        {
            _agent.isStopped = true;
        }



        //dead
        if (health <= 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }

      

        // healthBar
        healthBar.value = health;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
        }
    }

}
