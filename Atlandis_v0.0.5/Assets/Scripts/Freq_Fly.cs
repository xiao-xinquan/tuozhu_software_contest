using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freq_Fly : MonoBehaviour
{
    public float audioValue = 0f;
    public float volume;
    public float min_Volume = 0.1f;//ʶ��Ϊ�ڳ���
    public float freq_low = 0.1f;//Ƶ�����
    public float freq_high = 20f;//Ƶ�����

    private Vector2 target;//��������Ƶ��ȷ����Ŀ��߶�

    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed = 1f;
    private int pointIndex = 1;  //���ȡֵ
    private float waitTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(points[pointIndex].transform.position.x, points[pointIndex].transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        audioValue = Mic_Input.audioValue;
        volume = Mic_Input.volume;

        if (volume > min_Volume){
            if (audioValue > freq_high){
                audioValue = freq_high;
            }
            if (audioValue < freq_low)
            {
                audioValue = freq_low;
            }


            target.y = (audioValue - freq_low) / (freq_high - freq_low) * (points[0].transform.position.y - points[1].transform.position.y) + points[1].transform.position.y;


            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, points[pointIndex].transform.position) < 0.1f)
            {
                if (waitTime <= 0)
                {
                    waitTime = 0.5f;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}
