using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanelManager : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonPanelPair
    {
        public Button button;
        public GameObject panel;
    }

    public ButtonPanelPair[] buttonPanelPairs;
    private Dictionary<Button, GameObject> buttonToPanelMap = new Dictionary<Button, GameObject>();
    private GameObject currentActivePanel = null;

    void Start()
    {
        // 初始化按钮到面板的映射并设置初始状态
        foreach (var pair in buttonPanelPairs)
        {
            buttonToPanelMap[pair.button] = pair.panel;
            // 初始化时关闭所有面板
            pair.panel.SetActive(false);
            // 为每个按钮添加点击事件
            pair.button.onClick.AddListener(() => TogglePanel(pair.button));
        }
    }

    void TogglePanel(Button clickedButton)
    {
        GameObject panelToToggle;
        if (buttonToPanelMap.TryGetValue(clickedButton, out panelToToggle))
        {
            // 如果点击的是当前已激活的面板对应的按钮，则关闭该面板
            if (currentActivePanel == panelToToggle)
            {
                panelToToggle.SetActive(false);
                currentActivePanel = null;
            }
            else
            {
                // 关闭当前激活的面板（如果有）
                if (currentActivePanel != null)
                {
                    currentActivePanel.SetActive(false);
                }
                // 显示新的面板
                panelToToggle.SetActive(true);
                currentActivePanel = panelToToggle;
            }
        }
    }
}
