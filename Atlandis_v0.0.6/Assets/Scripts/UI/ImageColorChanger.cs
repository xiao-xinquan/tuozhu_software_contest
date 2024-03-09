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
        // ��ʼʱ��ȡ����ˮƽλ��
        initX = cursorRectTransform.anchoredPosition.x;
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }
    }

    private void Update()
    {
        // ����Image�Ҷ˵�λ��
        float imageRightEnd = targetImage.rectTransform.anchoredPosition.x + targetImage.rectTransform.rect.width;
        initX = cursorRectTransform.anchoredPosition.x;

        // ���Image�Ҷ��Ƿ񳬹��˹��
        if (imageRightEnd > initX)
        {
            targetImage.color = Color.green;
            //speakVolume = Mic_Input.volume;
        }
        else
        {
            // �����Ҫ�����Իָ���ԭʼ��ɫ
            targetImage.color = Color.white;
        }
    }
}
