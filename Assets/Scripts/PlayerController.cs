using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool isPlayer1Turn;
    public bool isDragging;
    public bool canDrag;
    GameObject currentTouchingObject;
    Vector2 movement;
    Rigidbody2D rb;
    
    public Animator p1Animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    float jump = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isPlayer1Turn = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if(Input.GetKeyDown(KeyCode.Space) && canDrag)
        {
            if(!isDragging)
            {
                isDragging = true;
                currentTouchingObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                isDragging = false;
                currentTouchingObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
        
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

    }

    private void FixedUpdate()
    {
        rb.velocity = movement * speed;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Push"))
        {
            canDrag = true;
            currentTouchingObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Push"))
        {
            if (currentTouchingObject != null)
            {
                currentTouchingObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                currentTouchingObject = null;
            }
            canDrag = false;
            isDragging = false;
        }
    }

  





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
