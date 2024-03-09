using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public Button[] buttons; // ��Inspector�������������

    void Start()
    {
        // Ϊÿ����ť��ӵ���¼�����
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i + 1; // ���㰴ť���
            buttons[i].onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
    }

    void ButtonClicked(int buttonNumber)
    {
        // ����ť����¼�
        Debug.Log("Button " + buttonNumber + " clicked.");
        if (buttonNumber <= dataController.unlockIndex)
        {
            SceneManager.LoadScene(buttonNumber);
        }
    }
}
