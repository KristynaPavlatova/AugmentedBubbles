using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer : MonoBehaviour
{
    public List<AudioClip> bubblePops = new List<AudioClip>();

    private void Start()
    {
        BubbleClass.BubblePopped += PlayPopAudio;
    }
    public void PlayPopAudio()
    {
        int index = (int)Random.Range(0, bubblePops.Count - 1);
        this.GetComponent<AudioSource>().clip = bubblePops[index];
        this.GetComponent<AudioSource>().Play();
    }
}
