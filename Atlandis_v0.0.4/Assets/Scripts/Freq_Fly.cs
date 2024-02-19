using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freq_Fly : MonoBehaviour
{
    private freqControl FreqControl;

    private void Start()
    {
        FreqControl = GetComponent<freqControl>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FlyBubble"))
        {
            Debug.Log("Enter player trigger");
            FreqControl.playerFly();
        }
    }

}
