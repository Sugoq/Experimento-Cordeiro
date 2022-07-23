using UnityEngine;

public class P1Controller : MonoBehaviour
{
    public static P1Controller instance;
    
    [SerializeField] Transform playerFoot1, playerFoot2;
    Animator p1Animator;
    Rigidbody2D rb;
    
    public int maxSwitchTimes;

    
    public float speed;
    public float gravityIncrease;
    public float jumpForce = 0;
    public float rayDistance;
    public bool isOnGround;
    public bool isJumping;
    
    private float movement;
    private int switchTimes;
    
    public LayerMask groundLayer;

    private void Awake() => instance = this;

    private void OnDestroy() => instance = null;



    void Start()
    {
        GroundCheck();
        rb = GetComponent<Rigidbody2D>();
        p1Animator = GetComponent<Animator>();
        Physics2D.gravity = new Vector2(0, gravityIncrease);
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        movement = Input.GetAxisRaw("Horizontal");
              
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (switchTimes >= maxSwitchTimes) return;
            switchTimes++;
            p1Animator.SetBool("Walk", false);
            movement = 0;
            
            SwitchCharacter.instance.Switch();
            rb.velocity = new Vector2(movement, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isOnGround && rb.velocity.y < 1)
            {
                isJumping = true;
                Jump();
            }
        }       
    }

    public void ChangeBodyType()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
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
        print(Physics2D.gravity);
    }

}
