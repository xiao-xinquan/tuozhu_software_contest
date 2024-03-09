using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freqControl : MonoBehaviour
{
    public float audioValue = 0f;
    public float volume;
    public float min_Volume = 0.1f;//识别为在出声
    public float freq_low = 0.1f;//频率最低
    public float freq_high = 20f;//频率最高

    private Vector2 target;//根据声音频率确定的目标高度

    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed = 1f;
    private int pointIndex = 1;  //点的取值
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
        min_Volume = dataController.speakVolume;


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
