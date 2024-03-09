using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mic_Input : MonoBehaviour
{
    public static float volume = 0f;
    public static float audioValue;
    AudioSource audioSource;
    //public float spectrumSize = 32;

    AudioClip micRecord;
    //AudioSource m_MyAudioSource;

    public float[] _audioSpectrum;
    public float sampleRate = 44100;
    public int sampleSize = 1024; // FFT的大小，必须是2的幂


    string device;

    //public int frameBuffer = 5;//每frameBuffer帧更新一次音高数据
    //private int frameCount;

    void Start()
    {
        device = Microphone.devices[0];
        micRecord = Microphone.Start(device, true, 999, 44100);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = micRecord;
        audioSource.loop = true; // 循环播放

        while (!(Microphone.GetPosition(device) > 0)) { }
        audioSource.Play();

        //frameCount = frameBuffer - 1;
    }

    // Update is called once per frame
    void Update()
    {
        volume = GetMaxVolume();

        audioValue = GetFundamentalFrequency();
        Debug.Log("Detected pitch: " + audioValue + " Hz");

        //frameCount++;
        /*if (frameCount == frameBuffer)
        {
            //AudioListener.GetSpectrumData(_audioSpectrum, 0, FFTWindow.BlackmanHarris);
            audioValue = GetFreq(_audioSpectrum);
            frameCount = 0;
        }*/
    }
    float GetMaxVolume()
    {
        float maxVolume = 0f;
        float[] volumeData = new float[128];
        int offset = Microphone.GetPosition(device) - 128 + 1;
        if (offset < 0)
        {
            return 0;
        }
        micRecord.GetData(volumeData, offset);
        for (int i = 0; i < 128; i++)
        {
            float tempMax = volumeData[i];
            if (tempMax > maxVolume)
            {
                maxVolume = tempMax;
            }
        }
        return maxVolume;
    }

    float GetFundamentalFrequency()
    {
        float[] spectrum = new float[sampleSize];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            // 找到最大幅度及其索引
            if (spectrum[i] > maxV && i > 0)
            {
                maxV = spectrum[i];
                maxN = i; // 最大幅度的索引
            }
        }
        // 计算频率
        float freqN = maxN;
        if (maxN > 0 && maxN < spectrum.Length - 1)
        {
            // 对最大点周围的点进行插值，以获得更准确的频率估计
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        // 频率（赫兹）
        float pitch = freqN * (sampleRate / 2) / spectrum.Length;
        return pitch;
    }
}
