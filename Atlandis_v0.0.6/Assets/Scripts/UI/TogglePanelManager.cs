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
        // ��ʼ����ť������ӳ�䲢���ó�ʼ״̬
        foreach (var pair in buttonPanelPairs)
        {
            buttonToPanelMap[pair.button] = pair.panel;
            // ��ʼ��ʱ�ر��������
            pair.panel.SetActive(false);
            // Ϊÿ����ť��ӵ���¼�
            pair.button.onClick.AddListener(() => TogglePanel(pair.button));
        }
    }

    void TogglePanel(Button clickedButton)
    {
        GameObject panelToToggle;
        if (buttonToPanelMap.TryGetValue(clickedButton, out panelToToggle))
        {
            // ���������ǵ�ǰ�Ѽ��������Ӧ�İ�ť����رո����
            if (currentActivePanel == panelToToggle)
            {
                panelToToggle.SetActive(false);
                currentActivePanel = null;
            }
            else
            {
                // �رյ�ǰ�������壨����У�
                if (currentActivePanel != null)
                {
                    currentActivePanel.SetActive(false);
                }
                // ��ʾ�µ����
                panelToToggle.SetActive(true);
                currentActivePanel = panelToToggle;
            }
        }
    }
}
