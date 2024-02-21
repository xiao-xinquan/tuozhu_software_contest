using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class HighlightButton : MonoBehaviour
{
    public Button button; // ���ð�ť
    public Color normalColor = Color.white; // ������ɫ
    public Color highlightColor = Color.yellow; // ������ɫ

    void Start()
    {
        button.onClick.AddListener(() => SetColor(highlightColor)); // Ϊ����¼���Ӽ�����
    }

    public void OnPointerEnter()
    {
        SetColor(highlightColor); // �����ͣʱ������ɫ
    }

    public void OnPointerExit()
    {
        SetColor(normalColor); // ����뿪ʱ�ָ���ɫ
    }

    void SetColor(Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        button.colors = cb; // Ӧ����ɫ����
    }
}