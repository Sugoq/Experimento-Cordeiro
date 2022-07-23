using UnityEngine;

public class ObstaclesConfigs : MonoBehaviour
{
    public float dragSpeed;
    public bool limitDrags;
    public int maxDrags;
    public bool canDrag;
    [HideInInspector] public int dragTimes;

    private void Start()
    {
        canDrag = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("P1"))
            canDrag = false;

        else return;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("P1"))
            canDrag = true;
        
        else return;
    }
}
