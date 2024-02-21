using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Collect : MonoBehaviour
{
    public GameObject collectedEffect;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        collectedEffect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _spriteRenderer.enabled = false;   
            _collider.enabled = false;  

            collectedEffect.SetActive(true);

            Destroy(gameObject, 0.2f);
            Destroy(collectedEffect, 0.5f);
        }
    }
}
