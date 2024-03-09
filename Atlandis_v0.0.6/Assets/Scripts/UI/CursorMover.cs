using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorMover : MonoBehaviour, IDragHandler
{
    public RectTransform cursorRectTransform;
    public RectTransform blockRectTransform;
    public static float speakVolume;
    public Canvas canvas;

    public float blockX;
    public float leftest;
    public float rightest;

    public float volume;

    private void Start()
    {
    }

    public void Update()
    {
        
        //Vector3 worldPosition = rectTransform.TransformPoint(rectTransform.rect.center);

        speakVolume = (cursorRectTransform.anchoredPosition.x - blockRectTransform.anchoredPosition.x) / 1000;

        volume=Mic_Input.volume;

        // ��ӡ������̨
        //Debug.Log("PosX: " + posX + ", PosY: " + posY);
    }



    public void OnDrag(PointerEventData eventData)
    {
        // ��ȡ�������Ļ�ϵ�λ��
        Vector2 pos = eventData.position;
        Vector2 localPos = cursorRectTransform.anchoredPosition;
        float clampedX;

        blockX = blockRectTransform.anchoredPosition.x;
        


        clampedX = Mathf.Clamp(localPos.x, leftest, rightest);
        // ����Canvas�ĵ�ǰ��������
        Camera eventCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pos, eventCamera, out localPos))
        {
            // ����Canvas�ĳߴ綯̬����leftest��rightest
            //float leftest = -canvas.GetComponent<RectTransform>().sizeDelta.x / 2; // ����Canvas��ȵ�һ��Ϊ��߽�
            //float rightest = canvas.GetComponent<RectTransform>().sizeDelta.x / 2; // ����Canvas��ȵ�һ��Ϊ�ұ߽�

            leftest = blockRectTransform.anchoredPosition.x + 1f;
            rightest = blockRectTransform.anchoredPosition.x + 999f;

            // Ӧ�ñ߽�����
            clampedX = Mathf.Clamp(localPos.x, leftest, rightest);

            // ����λ��
            cursorRectTransform.anchoredPosition = new Vector2(clampedX, cursorRectTransform.anchoredPosition.y);
        }
    }
}
