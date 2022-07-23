using UnityEngine;

public class P2Controller : MonoBehaviour
{
    public static P2Controller instance;
    
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float exitObjectTime;
    GameObject currentTouchingObject;
    Rigidbody2D rb;
    Transform dragObject;
    Vector2 movement;

    private int myLayer;
    private float speed;
    public float playerSpeed;
    public bool isDragging;
    public bool canDrag;
    private void Awake() => instance = this;

    private void OnDestroy() => instance = null;




    void Start()
    {
        myLayer = gameObject.layer;
        speed = playerSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            rb.velocity = Vector2.zero;
            if (isDragging) DragOff(0);
            SwitchCharacter.instance.Switch();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDragging) DragOn();
            else DragOff(exitObjectTime);
        }

        
    }

    private void DragOff(float timer)
    {

        Invoke("EnableCollider", timer);
        dragObject.SetParent(null);
        isDragging = false;
        speed = playerSpeed;
        boxCollider.size = Vector2.zero;
        boxCollider.isTrigger = true;
        dragObject.GetComponent<BoxCollider2D>().enabled = true;
        boxCollider.enabled = false;
        gameObject.layer = myLayer;
    }

    void EnableCollider() => circleCollider.enabled = true;

    private void DragOn()
    {
        if (currentTouchingObject == null) return;
        if (dragObject.GetComponent<ObstaclesConfigs>().limitDrags && dragObject.GetComponent<ObstaclesConfigs>().dragTimes >= dragObject.GetComponent<ObstaclesConfigs>().maxDrags) return;

        print("Entrando no primeiro if");
        isDragging = true;
        //Ativando BoxCollider do fanstasma para imitar a collider do objeto
        circleCollider.enabled = false;
        boxCollider.enabled = true;
        //Copiando o tamanho e desativando o collider do objeto
        boxCollider.size = new Vector2(dragObject.GetComponent<BoxCollider2D>().size.x * dragObject.localScale.x, dragObject.GetComponent<BoxCollider2D>().size.y * dragObject.localScale.y);
        dragObject.GetComponent<BoxCollider2D>().enabled = false;
        boxCollider.isTrigger = false;
       
        //Settando a posicao do player ao centro do objeto
        transform.SetParent(dragObject);
        transform.localPosition = Vector2.zero;
        transform.parent = null;
        dragObject.parent = transform;
        dragObject.localPosition = Vector2.zero;

        //Igualando a Layer para colidir com o player
        gameObject.layer = dragObject.gameObject.layer;
        
        //Pegando a velocidade que o fantasma pode movimentar o objeto
        speed = dragObject.GetComponent<ObstaclesConfigs>().dragSpeed;
        dragObject.GetComponent<ObstaclesConfigs>().dragTimes++;
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Push"))
        {
            if (collision.gameObject.GetComponent<ObstaclesConfigs>().canDrag == false) return;
            currentTouchingObject = collision.gameObject;
            dragObject = currentTouchingObject.transform;
        }
        else return;
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Push"))
        {
            if (currentTouchingObject != null)
            {
                currentTouchingObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                currentTouchingObject = null;
            }
        }
        else return;

    }
}