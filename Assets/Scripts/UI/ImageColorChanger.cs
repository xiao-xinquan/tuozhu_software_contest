using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour
{
    public RectTransform cursorRectTransform;
    public Image targetImage;
    //public float speakVolume;

    private float initX;

    private void Start()
    {
        // 初始时获取光标的水平位置
        initX = cursorRectTransform.anchoredPosition.x;
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }
    }

    private void Update()
    {
        // 计算Image右端的位置
        float imageRightEnd = targetImage.rectTransform.anchoredPosition.x + targetImage.rectTransform.rect.width;
        initX = cursorRectTransform.anchoredPosition.x;

        // 检查Image右端是否超过了光标
        if (imageRightEnd > initX)
        {
            targetImage.color = Color.green;
            //speakVolume = Mic_Input.volume;
        }
        else
        {
            // 如果需要，可以恢复到原始颜色
            targetImage.color = Color.white;
        }
    }
}
