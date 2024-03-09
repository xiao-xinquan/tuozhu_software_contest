using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hurt_Detect : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        if (collision.gameObject.tag == "trap")
        {
            animator.Play("Hurt");
            SceneManager.LoadScene(buildIndex);
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        if (collision.gameObject.tag == "trap")
        {
            animator.Play("Hurt");
            SceneManager.LoadScene(buildIndex);
        }
    }

}
