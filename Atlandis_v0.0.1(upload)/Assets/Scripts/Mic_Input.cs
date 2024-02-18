using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mic_Input : MonoBehaviour
{
    public static float volume;
    AudioClip micRecord;
    AudioSource m_MyAudioSource;

    private float[] _audioSpectrum;
    public static float spectrumValue { get; private set; }

    string device;
    // Start is called before the first frame update
    void Start()
    {
        device = Microphone.devices[0];
        m_MyAudioSource = GetComponent<AudioSource>();
        device = Microphone.devices[0];
        m_MyAudioSource.clip = Microphone.Start(device, true, 999, 44100);
        micRecord = Microphone.Start(device, true, 999, 44100);
        _audioSpectrum = new float[256];
    }

    // Update is called once per frame
    void Update()
    {
        volume=GetMaxVolume();
        //m_MyAudioSource.clip = micRecord;
        m_MyAudioSource.GetSpectrumData(_audioSpectrum, 0, FFTWindow.Hamming);
        if (_audioSpectrum != null && _audioSpectrum.Length > 0)
        {
            spectrumValue = _audioSpectrum[0] * 100;
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
        micRecord.GetData(volumeData,offset);
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
}
