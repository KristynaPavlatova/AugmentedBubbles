using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detecting the length of any audio clip to determine the size of a bubble.
public class AudioLoundnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;//the amount of data collected before the audio clip position where the loudness is checked
    private AudioClip microphoneClip;

    private void Start()
    {
        MicrophoneToAudioClip();
    }

    /// <summary>
    /// This function checks the "spikes" in an audio clip where the loudness is checked and will be returned.
    /// </summary>
    /// <param name="clipPosition">Represeants the position in the audio clip where we are checking the loudness.</param>
    /// <param name=""></param>
    /// <returns></returns>
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {        
        if (sampleWindow < 0)
            return 0;

        int startPosition = clipPosition - sampleWindow;
        float[] waveData = new float[sampleWindow];//for collecting the data within the defined sample window. Ranges from -1 to 1 (0 = no sound).
        clip.GetData(waveData, startPosition);

        //Compute loudness
        float totalLoudness = 0;

        for(int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);//always positive num

        }

        return totalLoudness / sampleWindow;//return average
    }
    public float GetLoudnessFromMicrophone()
    {
        float loudness = 0;
        loudness = GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
        
        if (loudness != 0) return loudness;
        else return 0;
    }

    public void MicrophoneToAudioClip()
    {
        //Get the first microphone in device list
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }
}
