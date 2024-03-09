using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mic_Input : MonoBehaviour
{
    public static float volume = 0f;
    public static float audioValue;
    //public float spectrumSize = 32;

    AudioClip micRecord;
    //AudioSource m_MyAudioSource;

    public float[] _audioSpectrum;


    string device;

    public int frameBuffer = 5;//每frameBuffer帧更新一次音高数据
    private int frameCount;

    void Start()
    {
        device = Microphone.devices[0];
        //m_MyAudioSource = GetComponent<AudioSource>();
        //m_MyAudioSource.clip = Microphone.Start(device, true, 999, 44100);
        micRecord = Microphone.Start(device, true, 999, 44100);
        _audioSpectrum = new float[64];

        frameCount = frameBuffer - 1;
    }

    // Update is called once per frame
    void Update()
    {
        volume = GetMaxVolume();
        

        frameCount++;
        if (frameCount == frameBuffer)
        {
            AudioListener.GetSpectrumData(_audioSpectrum, 0, FFTWindow.BlackmanHarris);
            audioValue = GetFreq(_audioSpectrum);
            frameCount = 0;
        }
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

    float GetFreq(float[] _audioSpectrum)
    {
        float freq = 0f;
        float amplitude = 0f;
        for (int i = 0; i < 64; i++)
        {
            if (_audioSpectrum[i]*100000000 > amplitude)
            {
                amplitude = _audioSpectrum[i]*100000000;
                freq = (float)i;
            }
        }
        return freq;
    }
}
