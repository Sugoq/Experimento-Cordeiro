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
    
    Animator p1Animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    void Start()
    {
        GroundCheck();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        p1Animator = GetComponent<Animator>();
        Physics2D.gravity *= gravityIncrease;
    }

    // Update is called once per frame
    void Update()
    {

        movement = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetKeyDown(KeyCode.F))
            p1Animator.SetBool("IsOnGround", true);


        if (Input.GetKeyDown(KeyCode.E))
        {
            p1Animator.SetBool("Walk", false);
            movement = 0;
            SwitchCharacter.instance.Switch();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping) return;
            if (!isJumping)
            {
                if(!isOnGround)return;
            }
            
            isJumping = true;
            print("jumped");
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
    }

    private void FixedUpdate()
    {
        isOnGround = GroundCheck();
        Debug.DrawRay(playerFoot1.position, Vector2.down *rayDistance, Color.green);
        Debug.DrawRay(playerFoot2.position, Vector2.down * rayDistance, Color.green);
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }

    public bool GroundCheck()
    {
        
        Vector2 direction = Vector2.down;
        RaycastHit2D hit1 = Physics2D.Raycast(playerFoot1.position, direction, rayDistance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(playerFoot2.position, direction, rayDistance, groundLayer);

        if (hit1.collider != null || hit2.collider != null)
        {          
            isJumping = false;
            return true;
        }

        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagsController>().HasTag("Ground"))
        {
            if (!isOnGround) return;
            print("e");
            p1Animator.SetBool("Jump", false);
            p1Animator.SetBool("IsOnGround", true);
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}
