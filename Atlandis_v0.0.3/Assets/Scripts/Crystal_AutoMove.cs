using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_AutoMove : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float startWaitTime = 2f;
    public float moveSpeed = 2f;
    public Transform[] patrolPoints;
    private float timer; // 计时器
    private int index = 1; // 当前巡逻点索引
    
    // Start is called before the first frame update
    void Start()
    {
        timer = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[index].position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, patrolPoints[index].position) < 0.1f)
        {
            if (timer <= 0)
            {
                if (patrolPoints[index] != patrolPoints[patrolPoints.Length - 1])
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                timer = startWaitTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sprite.color = new Color(1, 1, 1, 0.5f);
            Destroy(gameObject, 0.7f);
        }
    }

    private void CheckMove()
    {
        if (transform.position.x > patrolPoints[index].position.x)
        {
            sprite.flipX = true;
        }
        else if (transform.position.x < patrolPoints[index].position.x)
        {
            sprite.flipX = false;
        }
        else
        {
            return;
        }
    }
}
