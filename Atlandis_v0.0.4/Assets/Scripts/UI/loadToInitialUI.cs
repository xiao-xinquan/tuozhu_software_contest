using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadToInitialUI : MonoBehaviour
{
    public void Jump()
    {
        SceneManager.LoadScene(0);
    }
}
