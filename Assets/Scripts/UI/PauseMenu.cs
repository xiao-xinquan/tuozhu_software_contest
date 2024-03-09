using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // ��ͣ�˵�UI����
    public bool isPaused; // ��ͣ״̬��־

    private void Start()
    {
        pauseMenuUI.SetActive(false); // ���ֲ���ʾ��ͣ�˵�
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ����ESC��ͳ����ͣ
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0f; // ��ͣ��Ϸ
            pauseMenuUI.SetActive(true); // ��ʾ��ͣ�˵�
        }
        else
        {
            Time.timeScale = 1f; // �ָ���Ϸ
            pauseMenuUI.SetActive(false); // ������ͣ�˵�
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