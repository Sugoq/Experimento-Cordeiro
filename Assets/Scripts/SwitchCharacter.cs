using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public static SwitchCharacter instance;
    
    public bool isP1Turn;
    public GameObject p1, p2;
    public int maxSwitchTimes;
    int switchTimes;

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
        if (switchTimes == maxSwitchTimes) return;
        
        if (isP1Turn)
        {
            p1.GetComponent<P1Controller>().enabled = false;
            p2.GetComponent<P2Controller>().enabled = true;
            isP1Turn = false;
            switchTimes++;
            
        }
        else
        {
            p1.GetComponent<P1Controller>().enabled = true;
            p2.GetComponent<P2Controller>().enabled = false;
            isP1Turn = true;
            switchTimes++;

        }
    }
}
