using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    [SerializeField] bool isOnGround;
    public float speed;
    public float gravityIncrease;
    Animator p1Animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public float jumpForce = 0;
    float movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        p1Animator = GetComponent<Animator>();
        Physics2D.gravity *= gravityIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            p1Animator.SetBool("Walk", false);
            movement = 0;
            SwitchCharacter.instance.Switch();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!isOnGround) return;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            p1Animator.SetBool("Jump", true);
            p1Animator.SetBool("IsOnGround", false);

            isOnGround = false;
        }

        if (movement > 0)
        {
            p1Animator.SetBool("Walk", true);
            spriteRenderer.flipX = false;
        }
        else if (movement < 0)
        {
            p1Animator.SetBool("Walk", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            p1Animator.SetBool("Walk", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagsController>().HasTag("Ground"))
        {
            isOnGround = true;
            p1Animator.SetBool("Jump", false);
            p1Animator.SetBool("IsOnGround", true);
        }
        else return;
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}
