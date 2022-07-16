using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator p1Animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    float jump = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            p1Animator.SetBool("Jump", true);
        }

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), jump);
        transform.Translate(movement * speed * Time.deltaTime);
        
        if(Input.GetAxis("Horizontal") > 0)
        {
            p1Animator.SetBool("Walk", true);
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            p1Animator.SetBool("Walk", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            p1Animator.SetBool("Walk", false);
        }                
    }

    void Jump()
    {
        jump = 5f;
    }

    void Down()
    {
        jump = 0;
    }

}
