using UnityEngine;
using UnityEngine.UI; // 引用UI命名空间

public class HighlightButton : MonoBehaviour
{
    public Button button; // 引用按钮
    public Color normalColor = Color.white; // 正常颜色
    public Color highlightColor = Color.yellow; // 高亮颜色

    void Start()
    {
        button.onClick.AddListener(() => SetColor(highlightColor)); // 为点击事件添加监听器
    }

    public void OnPointerEnter()
    {
        SetColor(highlightColor); // 鼠标悬停时设置颜色
    }

    public void OnPointerExit()
    {
        SetColor(normalColor); // 鼠标离开时恢复颜色
    }

    void SetColor(Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        button.colors = cb; // 应用颜色更改
    }
}