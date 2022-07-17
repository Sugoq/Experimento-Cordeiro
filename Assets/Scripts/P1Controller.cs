using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    [SerializeField] bool isOnGround;
    public float speed;
    public float gravityIncrease;
    Animator p2Animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public float jumpForce = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        p2Animator = GetComponent<Animator>();
        Physics2D.gravity *= gravityIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isOnGround) return;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            p2Animator.SetBool("Jump", true);
            p2Animator.SetBool("IsOnGround", false);

            isOnGround = false;
        }

        float movement = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * movement * speed * Time.deltaTime);

        if (movement > 0)
        {
            p2Animator.SetBool("Walk", true);
            spriteRenderer.flipX = false;
        }
        else if (movement < 0)
        {
            p2Animator.SetBool("Walk", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            p2Animator.SetBool("Walk", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagsController>().HasTag("Ground"))
        {
            isOnGround = true;
            p2Animator.SetBool("Jump", false);
            p2Animator.SetBool("IsOnGround", true);
        }
    }

}
