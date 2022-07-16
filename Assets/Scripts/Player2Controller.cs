using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public float gravityMultiplier;
    private bool isOnGround;
    public bool isPlayer2Turn;

    private void Start()
    {
        Physics2D.gravity *= gravityMultiplier;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * movement * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            Jump();
            isOnGround = false;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }

}
