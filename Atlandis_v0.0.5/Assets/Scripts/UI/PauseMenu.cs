using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // 暂停菜单UI对象
    public bool isPaused; // 暂停状态标志

    private void Start()
    {
        pauseMenuUI.SetActive(false); // 开局不显示暂停菜单
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // 按下ESC键统计暂停
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0f; // 暂停游戏
            pauseMenuUI.SetActive(true); // 显示暂停菜单
        }
        else
        {
            Time.timeScale = 1f; // 恢复游戏
            pauseMenuUI.SetActive(false); // 隐藏暂停菜单
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}