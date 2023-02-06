using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject BubblePrefab;
    public MicInput MicInputClass;
    [Space(10)]
    [Tooltip("The value the loudness must go above in order for the BubbleSpawner to start spawning bubbles.")]
    public float spawnThreshold = 0.1f;
    public float spawnDelay = 0.3f;
    [Space(10)]

    private bool _wasVeryFirsBubbleSpawned = false;
    private bool _wasANewBubbleSpawned = false;

    public delegate void OnFirstBubbleSpawned();
    public static event OnFirstBubbleSpawned VeryFirstBubbleSpawned;

    void Start()
    {
        Debug.Assert(BubblePrefab, $"BubblePrefab prefab is empty!");
        Debug.Assert(MicInputClass, $"MicInputCalss is not assigned!");


        InvokeRepeating("getMicLoudness", 1, 1);
    }
    
    void Update()
    {
        if (MicInputClass.GetRawMicLoudness() > spawnThreshold)
        {
            if (!_wasANewBubbleSpawned)
            {
                Invoke("spawnBubble", 0.0f);
                _wasANewBubbleSpawned = true;
                Invoke("newBubbleSpawnDelay", spawnDelay);

                if (!_wasVeryFirsBubbleSpawned)
                {
                    _wasVeryFirsBubbleSpawned = true;
                    VeryFirstBubbleSpawned();
                }  
            }
        }
    }

    private void getMicLoudness()
    {
        float micLoudness = MicInputClass.GetRawMicLoudness();
    }

    private void spawnBubble()
    {
        Instantiate(BubblePrefab, this.transform.position, this.transform.rotation);

    }
    private void newBubbleSpawnDelay()
    {
        _wasANewBubbleSpawned = false;//new bubble can be spawned
    }
}
