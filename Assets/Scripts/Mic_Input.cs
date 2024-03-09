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
    public int sampleSize = 1024; // FFT�Ĵ�С��������2����


    string device;

    //public int frameBuffer = 5;//ÿframeBuffer֡����һ����������
    //private int frameCount;

    void Start()
    {
        device = Microphone.devices[0];
        micRecord = Microphone.Start(device, true, 999, 44100);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = micRecord;
        audioSource.loop = true; // ѭ������

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
            // �ҵ������ȼ�������
            if (spectrum[i] > maxV && i > 0)
            {
                maxV = spectrum[i];
                maxN = i; // �����ȵ�����
            }
        }
        // ����Ƶ��
        float freqN = maxN;
        if (maxN > 0 && maxN < spectrum.Length - 1)
        {
            // ��������Χ�ĵ���в�ֵ���Ի�ø�׼ȷ��Ƶ�ʹ���
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        // Ƶ�ʣ����ȣ�
        float pitch = freqN * (sampleRate / 2) / spectrum.Length;
        return pitch;
    }
}
