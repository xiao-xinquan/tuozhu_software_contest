using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed = 1f;
    private int pointIndex = 1;  //点的取值
    private float waitTime = 0.5f;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, points[pointIndex].transform.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                if (pointIndex == 0)
                {
                    pointIndex = 1;
                }
                else
                {
                    pointIndex = 0;
                }
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

}