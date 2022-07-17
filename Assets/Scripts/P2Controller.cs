using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controller : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = Vector2.zero;
            Drag();
            SwitchCharacter.instance.Switch();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDrag)
        {
            Drag();
        }


    }

    private void Drag()
    {
        if (currentTouchingObject == null) return;

        
        if (!isDragging)
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

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TagsController>().HasTag("Push"))
        {
            canDrag = true;
            currentTouchingObject = collision.gameObject;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TagsController>().HasTag("Push"))
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