using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smoothing = 0.1f;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (player != null)
        {
            if (transform.position != player.position)
            {
                Vector3 targetPos = new Vector3(player.position.x, player.position.y + 2, transform.position.z);
                targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }    
    }
}