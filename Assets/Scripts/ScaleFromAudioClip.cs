using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class ScaleFromAudioClip : MonoBehaviour
{
    AudioSource micAudioSource;
    public AudioClip microphoneInputClip;
    public bool useMicrophone = true;
    public string selectedMicDevice;

    //https://www.youtube.com/watch?v=dzD0qP8viLw&ab_channel=ValemTutorials
    //7:28
    //public AudioSource source;
    [Space(10)]
    public Vector3 minScale;
    public Vector3 maxScale;
    [Space(10)]
    public AudioLoundnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;


    private void Awake()
    {
        //init microphone input
        if(useMicrophone && Microphone.devices.Length > 0)
        {
            selectedMicDevice = Microphone.devices[0].ToString();
            Debug.Log($"Selected microphone device = {selectedMicDevice}.");


            microphoneInputClip = Microphone.Start(selectedMicDevice, true, 10, AudioSettings.outputSampleRate);
        }

        //any microphone connected?
        if (Microphone.devices.Length > 0)
        {
            selectedMicDevice = Microphone.devices[0].ToString();
            Debug.Log($"Selected microphone device = {selectedMicDevice}.");

            Debug.Assert(micAudioSource, $"MicAudioSource is null!");
            micAudioSource = this.GetComponent<AudioSource>();
            micAudioSource.clip = Microphone.Start(selectedMicDevice, true, 10, AudioSettings.outputSampleRate);
            microphoneInputClip = micAudioSource.clip;
        }

        micAudioSource.Play();
    }

    void Update()
    {
        //get mic volume
        int dec = 128;//num of audio samples in the clip
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(null) - (dec + 1);//null = the 1st microphone
        microphoneInputClip.GetData(waveData, micPosition);

        //Getting a peakk on the last 128 samples
        float maxLevel = 0;
        for(int i = 0; i < dec; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (maxLevel < wavePeak)
                maxLevel = wavePeak;
        }
        float level = Mathf.Sqrt(Mathf.Sqrt(maxLevel));
        Debug.Log($"Audio level = {level}");

        //now check the audio level
        if (level > loudnessSensibility)
        {
            Debug.LogWarning("It is loud!");
        }

        if (useMicrophone)
            if (micAudioSource.isPlaying)
                Debug.Log("Mic Audio source is Playing.");
        
        float loudness = 0.0f;
        if (!useMicrophone)
        {
            loudness = detector.GetLoudnessFromAudioClip(micAudioSource.timeSamples, micAudioSource.clip) * loudnessSensibility;
        }
        else loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        
        if (loudness < threshold)
            loudness = 0;
        
        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }    
}