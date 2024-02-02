using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float volume;

    Rigidbody2D rg;

    public float jumpForce = 500;
    public float moveForce = 10;
    public float movevalue = 0.1f;
    public float jumpvalue = 0.4f;
    public float maxSpeed = 5;
    float tempTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rg = /*this.gameObject.*/GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        volume = MicroInput.volume;

        if(volume > movevalue) {
            MoveForward();
            if(rg.velocity.x > maxSpeed){
                rg.velocity = new Vector2(maxSpeed, rg.velocity.y);
            }
        }

        if(volume > jumpvalue) {
            if(Time.time - tempTime > 2)
            {
                Jump();
                tempTime = Time.time;
            }
            
        }
    }

    void Jump()
    {
        rg.AddForce(Vector2.up * jumpForce * volume);
    }

    void MoveForward()
    {
        rg.AddForce(Vector2.right * moveForce);
    }
}
