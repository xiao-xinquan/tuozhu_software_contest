using UnityEngine;
using UnityEngine.UI;

public class DynamicWidthUI : MonoBehaviour
{
    public Image targetImage; // ������Ҫ�ı��ȵ�UI Image
    public float maxWidth = 1000; // UI Image�������
    public float externalParameter; // �ⲿ��������������������������
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



    // ����UI Image�Ŀ��
    void Update()
    {
        externalParameter = Mic_Input.volume;
        UpdateWidthBasedOnExternalParameter();

        float posX = rectTransform.anchoredPosition.x;
        float posY = rectTransform.anchoredPosition.y;

        // ��ӡ������̨
        //Debug.Log("block  PosX: " + posX + ", PosY: " + posY);

    }

    // �����ⲿ����������ȵķ���
    void UpdateWidthBasedOnExternalParameter()
    {
        if (targetImage != null)
        {
            // �����µĿ��ֵ���������externalParameter�ķ�Χ��0��1
            float newWidth = maxWidth * externalParameter;

            // ����UI Image�Ŀ��
            targetImage.rectTransform.sizeDelta = new Vector2(newWidth, targetImage.rectTransform.sizeDelta.y);
            //targetImage.rectTransform.anchoredPosition = new Vector2(posX, posY);
        }
    }
}

