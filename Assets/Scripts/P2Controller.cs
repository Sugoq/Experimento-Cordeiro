using UnityEngine;

public class P2Controller : MonoBehaviour
{
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] CircleCollider2D circleTrigger;


    public float playerSpeed;
    [SerializeField] float speed;
    public bool isPlayer1Turn;
    public bool isDragging;
    public bool canDrag;
    GameObject currentTouchingObject;
    Transform drag;
    Vector2 movement;
    Rigidbody2D rb;


    void Start()
    {
        speed = playerSpeed;
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
            SwitchCharacter.instance.Switch();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDragging) DragOn();
            else DragOff();
        }

        
    }

    private void DragOff()
    {
        print("e");

        drag.SetParent(null);
        Invoke("EnableCollider", 0.2f);
        isDragging = false;
        speed = playerSpeed;
    }

    void EnableCollider() => circleCollider.enabled = true;

    private void DragOn()
    {
        if (currentTouchingObject == null) return;
        if (drag.GetComponent<ObstaclesConfigs>().limitDrags && drag.GetComponent<ObstaclesConfigs>().dragTimes >= drag.GetComponent<ObstaclesConfigs>().maxDrags) return;

        print("Entrando no primeiro if");
        isDragging = true;
        circleCollider.enabled = false;
        transform.SetParent(drag);
        print(drag);
        transform.localPosition = Vector2.zero;
        transform.parent = null;
        drag.parent = transform;
        drag.localPosition = Vector2.zero;
        speed = drag.GetComponent<ObstaclesConfigs>().dragSpeed;
        drag.GetComponent<ObstaclesConfigs>().dragTimes++;
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagsController>().HasTag("Push"))
        {
            currentTouchingObject = collision.gameObject;
            drag = currentTouchingObject.transform;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagsController>().HasTag("Push"))
        {
            if (currentTouchingObject != null)
            {
                currentTouchingObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                currentTouchingObject = null;
            }
        }

    }
}