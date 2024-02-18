using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world position
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if there is any collider under the mouse position
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null  && hit.collider.gameObject.name == "Home_button")
            {
                // If there is a collider, call SceneTrans
                SceneTrans0();
            }
            if (hit.collider != null && hit.collider.gameObject.name == "Play_button")
            {
                // If there is a collider, call SceneTrans
                SceneTrans1();
            }
        }
    }

    public void SceneTrans0()
    {
        
        SceneManager.LoadScene("Initial_UI(Episode 0)");
        
    }

    public void SceneTrans1()
    {
        SceneManager.LoadScene("Episode1");
    }

}
