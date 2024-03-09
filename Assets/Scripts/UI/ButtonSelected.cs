using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonSelected : MonoBehaviour
{
    Color oldColor;
    private void Start()
    {
        oldColor = transform.GetComponent<Image>().tintColor;
    }


    public void OnMouseEnter()
    {
        transform.transform.GetComponent<Image>().tintColor = Color.yellow;
    }

    private void OnMouseExit()
    {
        transform.transform.GetComponent<Image>().tintColor = oldColor;
    }
}
