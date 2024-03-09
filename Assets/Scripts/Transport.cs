using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    private GameObject currentPortal;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPortal != null)
        {
            transform.position = currentPortal.GetComponent<Portal>().GetDestination().position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Portal")
        {
            currentPortal = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Portal")
        {
            if (other.gameObject == currentPortal)
            {
                currentPortal = null;
            }
        }
    }
}
