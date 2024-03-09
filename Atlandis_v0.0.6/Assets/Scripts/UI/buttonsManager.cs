using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public Button[] buttons; // 在Inspector中设置这个数组

    void Start()
    {
        // 为每个按钮添加点击事件监听
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i + 1; // 计算按钮编号
            buttons[i].onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
    }

    void ButtonClicked(int buttonNumber)
    {
        // 处理按钮点击事件
        Debug.Log("Button " + buttonNumber + " clicked.");
        if (buttonNumber <= dataController.unlockIndex)
        {
            SceneManager.LoadScene(buttonNumber);
        }
    }
}
