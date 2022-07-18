using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    [SerializeField] bool isOnGround;
    [SerializeField] Transform playerFoot1, playerFoot2;
    
    public float speed;
    public float gravityIncrease;
    public float jumpForce = 0;
    public LayerMask groundLayer;
    public float rayDistance;
    float movement;
    public bool isJumping;
    private bool jump;
    
    Animator p1Animator;
    Rigidbody2D rb;

    void Start()
    {
        GroundCheck();
        rb = GetComponent<Rigidbody2D>();
        p1Animator = GetComponent<Animator>();
        Physics2D.gravity *= gravityIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();


        
        
        movement = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetKeyDown(KeyCode.F))
            p1Animator.SetBool("IsOnGround", true);


        if (Input.GetKeyDown(KeyCode.E))
        {
            p1Animator.SetBool("Walk", false);
            movement = 0;
            rb.velocity = new Vector2(0, rb.velocity.y);
            SwitchCharacter.instance.Switch();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isOnGround && rb.velocity.y < 1)
            {
                isJumping = true;
                p1Animator.SetBool("Jump", true);
                p1Animator.SetBool("IsOnGround", false);
                Jump();
            }
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

        if (isOnGround && isJumping && rb.velocity.y <= 0.001f)
        {
            isJumping = false;
            p1Animator.SetBool("Jump", false);
            p1Animator.SetBool("IsOnGround", true);

        }

    }

    private void FixedUpdate()
    {
        
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }

    public void GroundCheck()
    {

        Vector2 direction = Vector2.down;
        Physics2D.queriesHitTriggers = false;
        RaycastHit2D hit1 = Physics2D.Raycast(playerFoot1.position, direction, rayDistance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(playerFoot2.position, direction, rayDistance, groundLayer);

        Debug.DrawRay(playerFoot1.position, Vector2.down * rayDistance, Color.green);
        Debug.DrawRay(playerFoot2.position, Vector2.down * rayDistance, Color.green);

        isOnGround = hit1 || hit2;
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}
