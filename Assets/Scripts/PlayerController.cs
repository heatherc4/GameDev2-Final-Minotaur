using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public int health = 10;

    Vector3 velocity;
    bool isGrounded;

    public Slider HealthBar;


    

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);



        //receive damage
        if (health <= 0)
        {
            health = 0;
        }

        // healthBar
        HealthBar.value = health;

        Lose();
    }




    //Hit by enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
            Debug.Log("1111");
        }
        
    }


    //Win
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "winBox")
        {
            SceneManager.LoadScene(2);
        }
    }

    //lose
    public void Lose()
    {
        if(health == 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
