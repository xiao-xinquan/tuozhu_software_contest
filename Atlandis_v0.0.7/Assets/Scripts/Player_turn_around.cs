using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_turn_around : MonoBehaviour
{
    Rigidbody2D rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        turnaround_detect();
    }

    public void turnaround_detect()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (horizontal >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
