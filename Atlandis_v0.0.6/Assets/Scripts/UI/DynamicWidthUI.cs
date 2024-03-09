using UnityEngine;
using UnityEngine.UI;

public class DynamicWidthUI : MonoBehaviour
{
    public Image targetImage; // 引用需要改变宽度的UI Image
    public float maxWidth = 1000; // UI Image的最大宽度
    public float externalParameter; // 外部参数，基于这个参数来调整宽度
    public RectTransform rectTransform;

    //public float posX;
    //public float posY;

    private void Start()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }



    // 更新UI Image的宽度
    void Update()
    {
        externalParameter = Mic_Input.volume;
        UpdateWidthBasedOnExternalParameter();

        float posX = rectTransform.anchoredPosition.x;
        float posY = rectTransform.anchoredPosition.y;

        // 打印到控制台
        //Debug.Log("block  PosX: " + posX + ", PosY: " + posY);

    }

    // 根据外部参数调整宽度的方法
    void UpdateWidthBasedOnExternalParameter()
    {
        if (targetImage != null)
        {
            // 计算新的宽度值，这里假设externalParameter的范围是0到1
            float newWidth = maxWidth * externalParameter;

            // 设置UI Image的宽度
            targetImage.rectTransform.sizeDelta = new Vector2(newWidth, targetImage.rectTransform.sizeDelta.y);
            //targetImage.rectTransform.anchoredPosition = new Vector2(posX, posY);
        }
    }
}

