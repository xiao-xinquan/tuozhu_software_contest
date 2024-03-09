using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadToNextScene_trigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Scene currentScene = SceneManager.GetActiveScene();

            int buildIndex = currentScene.buildIndex;

            if (buildIndex > 0 && buildIndex < 6)
            {
                SceneManager.LoadScene(buildIndex + 1);
            }
            
        }
    }
}
