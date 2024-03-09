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

        // 打印到控制台
        //Debug.Log("PosX: " + posX + ", PosY: " + posY);
    }



    public void OnDrag(PointerEventData eventData)
    {
        // 获取鼠标在屏幕上的位置
        Vector2 pos = eventData.position;
        Vector2 localPos = cursorRectTransform.anchoredPosition;
        float clampedX;

        blockX = blockRectTransform.anchoredPosition.x;
        


        clampedX = Mathf.Clamp(localPos.x, leftest, rightest);
        // 考虑Canvas的当前缩放因子
        Camera eventCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pos, eventCamera, out localPos))
        {
            // 根据Canvas的尺寸动态计算leftest和rightest
            //float leftest = -canvas.GetComponent<RectTransform>().sizeDelta.x / 2; // 假设Canvas宽度的一半为左边界
            //float rightest = canvas.GetComponent<RectTransform>().sizeDelta.x / 2; // 假设Canvas宽度的一半为右边界

            leftest = blockRectTransform.anchoredPosition.x + 1f;
            rightest = blockRectTransform.anchoredPosition.x + 999f;

            // 应用边界限制
            clampedX = Mathf.Clamp(localPos.x, leftest, rightest);

            // 更新位置
            cursorRectTransform.anchoredPosition = new Vector2(clampedX, cursorRectTransform.anchoredPosition.y);
        }
    }
}
