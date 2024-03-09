using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class freqDraw : MonoBehaviour
{
    private AudioSource mAudio;
    /// <summary>
    /// 存放频谱数据的数组长度，长度必须为2的n次方，最小64，最大8192
    /// </summary>
    [Range(64, 128 * 2)]
    public int _sampleLength = 128 * 2;
    [Range(10, 50)]
    public float Frequency = 20;
    /// <summary>
    /// 音频频率数组
    /// </summary>
    private float[] samples;
    public Color lineColor = Color.green;
    //private VectorLine audioLine;
    private List<Vector3> linepoints;
    private VectorLine audioLine;
    public int pointCount;
    public GameObject cubePrefab;
    private Transform[] cubeTransform;//用来生成于频谱同等数量的预制体组
    Vector3 cubePos;//中间位置，用来对比cube位置于此帧的频谱数据
 
    private void Start()
    {
        StartCoroutine(InitWhenMicrophoneReady());
    }
    private IEnumerator InitWhenMicrophoneReady()
    {
        
        mAudio = GetComponent<AudioSource>();
        mAudio.clip = Microphone.Start(null, true, 10, 44100);
        mAudio.loop = true;
        while (!(Microphone.GetPosition(null) > 0))
        {
            yield return null;
        }

        mAudio.Play();
        Init();
    }



    private void Init()
    {
        GameObject tempCube;
        samples = new float[_sampleLength];
        pointCount = _sampleLength;
        linepoints = new List<Vector3>(pointCount);
        cubeTransform = new Transform[samples.Length];
        transform.position = new Vector3(-samples.Length * 0.5f, transform.position.y, transform.position.z);
        print("画线初始化开始");
        audioLine = new VectorLine("audioline", linepoints, 5.0f, LineType.Continuous);
        //根据获取的音频长度生成预制体组，并初始化Cube的位置
        for (int i = 0; i < samples.Length; i++)
        {
            tempCube = Instantiate(cubePrefab, new Vector3(transform.position.x + i, transform.position.y, transform.position.z), Quaternion.identity);
            cubeTransform[i] = tempCube.transform;
            cubeTransform[i].parent = transform;
        }
        audioLine.color = lineColor;
        //audioLine.Draw();
        print("画线结束");
        //audioLine.drawTransform = transform;
    }


    private void Update()
	{
        if(mAudio.clip != null)
        {
            GetAudioData();
        }
    }

    /// <summary>
    /// 根据获取音频频谱更新cube的位置及画线让频谱可视化
    /// </summary>
    private void GetAudioData()
    {
        //获取频谱
        mAudio.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < samples.Length; i++)
        {
            //linepoints[i]=new Vector3(i, Mathf.Clamp(samples[i] * (50 + i * i * 0.5f), 0, 50), 1);
            //print(linepoints[i]);
            //设置每一个cube的位置
            var Pos = new Vector3(cubeTransform[i].position.x, Mathf.Clamp(samples[i] * (50 + i * i * 0.5f), 0, 100), cubeTransform[i].position.z);
            cubePos = Pos;
            //cubePos.Set(ScreenPos);//频谱的数据主要是用来设置cube的y值，使用Mathf.Clamp将y值限制在一定范围，避免过大
            //频谱值越向后越小，为避免后面的数据变化不明显，所以在扩大sample[i]时，乘以50+i*i*0.5f
            linepoints[i] = cubePos - Vector3.up;//控制线点的位置始终位于预制体下面
            if (cubeTransform[i].position.y < cubePos.y)
            {
                //当频谱位置大于当前预制体位置，则设为频谱位置
                cubeTransform[i].position = cubePos;
            }
            else if (cubeTransform[i].position.y > cubePos.y)
            {
                //当频谱位置小于当前预制体位置，则当前位置y值每次减少0.5，形成下落的效果
                cubeTransform[i].position -= new Vector3(0, 0.5f, 0);
            }
        }
        audioLine.Draw();
    }

}
