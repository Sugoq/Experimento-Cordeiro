using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject EndGate;
    public float blinkTIme;

    private void Start()
    {
        EndGate = GameObject.Find("EndGate");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("P1"))
            StartCoroutine(OpenGateRoutine());
    }

    IEnumerator OpenGateRoutine()
    {
        transform.position = Vector2.one * 1000;
        SpriteRenderer sr = EndGate.GetComponent<SpriteRenderer>();
        yield return null;
        for(float t = 0; t < 1; t+= Time.deltaTime / blinkTIme)
        {
            sr.enabled = !sr.enabled;
            yield return null;
        }
        Destroy(EndGate);
        Destroy(gameObject);
    }



}
