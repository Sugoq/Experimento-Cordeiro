using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator p1Animator;
    
    private float movement;
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] Transform foot1, foot2;
    LayerMask groundLayer;
    private float rayDistance;
    private bool isJumping;


    void Start()
    {
        p1Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rayDistance = P1Controller.instance.rayDistance;
        groundLayer = P1Controller.instance.groundLayer;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();


            if (P1Controller.instance.enabled == true)
            movement = Input.GetAxisRaw("Horizontal");
        else movement = 0;


        if (P1Controller.instance.isJumping)
        {
            isJumping = true;
            p1Animator.SetBool("Jump", true);
            p1Animator.SetBool("IsOnGround", false);
        }

        if (movement > 0)
        {
            p1Animator.SetBool("Walk", true);
            transform.localScale = Vector3.one;
        }
        else if (movement < 0)
        {
            p1Animator.SetBool("Walk", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            p1Animator.SetBool("Walk", false);
        }

        if (isGrounded && isJumping && rb.velocity.y <= 0.001f)
        {
            P1Controller.instance.isJumping = false;
            p1Animator.SetBool("Jump", false);
            p1Animator.SetBool("IsOnGround", true);

        }
    }

    void StaticRb()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void GroundCheck()
    {

        Vector2 direction = Vector2.down;
        Physics2D.queriesHitTriggers = false;
        RaycastHit2D hit1 = Physics2D.Raycast(foot1.position, direction, rayDistance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(foot2.position, direction, rayDistance, groundLayer);

        isGrounded = hit1 || hit2;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (P1Controller.instance.enabled == false && isGrounded)
        {
            StaticRb();
        }
    }
}
