using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Running : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float groundCheckDistance, wallCheckDistance;
    private bool groundDetect;
    private bool wallDetect;

    private LayerMask ground, wall;
    [SerializeField] private Transform groundCheck, wallCheck;
    private int facingDirection;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = LayerMask.GetMask("ground");
        wall = LayerMask.GetMask("wall");
        facingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetected();
    }

    private void CheckDetected()
    {
        groundDetect = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, ground);
        wallDetect = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, wall);

        if (wallDetect)
        {
            Flip();
        }
        if (groundDetect)
        {
            Move();
            if (rb.velocity.y < 0.1f)
            {
                Jump();
            }
        }
    
    }

    private void Flip()
    {
        facingDirection *= -1;
        rb.transform.Rotate(0f, 180f, 0f);
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
