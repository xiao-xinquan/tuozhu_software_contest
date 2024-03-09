using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_game : MonoBehaviour
{

    // Update is called once per frame
    public void gameContinue()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        int buildIndex = currentScene.buildIndex;

        if (buildIndex >= 0 && buildIndex <= 6)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}
