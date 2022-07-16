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

  





}
