using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dataController : MonoBehaviour
{
    // Start is called before the first frame update

    public static int buildIndex = 0;
    public static int unlockIndex = 1;
    public static float speakVolume;

    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        speakVolume = 0.3f;
    }

    // Update is called once per frame
    public void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene != null && currentScene.buildIndex!=7)
        {
            int buildIndex = currentScene.buildIndex;
        }
        if (currentScene.buildIndex >= 2 && currentScene.buildIndex <= 5)
        {
            unlockIndex = currentScene.buildIndex;
        }


        speakVolume = CursorMover.speakVolume;

    }

    void getToPrevScene()
    {
        SceneManager.LoadScene(buildIndex);
    }
}
