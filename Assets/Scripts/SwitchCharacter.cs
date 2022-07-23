using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public static SwitchCharacter instance;

    public bool isP1Turn;
    public GameObject p1, p2;

    private void Awake()
    {
        isP1Turn = true;
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void Switch()
    {
        if (isP1Turn)
        {
            p1.GetComponent<P1Controller>().enabled = false;
            p2.GetComponent<P2Controller>().enabled = true;
            isP1Turn = false;
        }
        else
        {
            p1.GetComponent<P1Controller>().enabled = true;
            p1.GetComponent<P1Controller>().ChangeBodyType();
            p2.GetComponent<P2Controller>().enabled = false;
            isP1Turn = true;
        }
    }
}
