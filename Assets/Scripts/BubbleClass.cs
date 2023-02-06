using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleClass : MonoBehaviour
{
    //public float forwardSpeed = 0.03f;
    //[Space(10)]
    //public float minUpSpeed = 0.01f;
    //public float maxUpSpeed = 0.05f;
    //[Space(10)]
    //public float gravityStrength = 0.0001f;
    //[Space(10)]
    //public int minLifetimeInSeconds = 3;
    //public int maxLifetimeInSeconds = 15;
    //[Space(10)]
    //[Tooltip("The 'field of view' but it determines the min and max angle the bubbles will fly forward in relation to the forward view vector.")]
    //public float bubbleSpawningFieldOfView = 90.0f;
    [Space(10)]
    public List<Material> bubbleMats = new List<Material>();
    [Space(10)]
    public ScriptableBubbleData bubbleData;

    private Vector3 trajectory;
    public delegate void OnBubblePop();
    public static event OnBubblePop BubblePopped;

    void Start()
    {
        //Trajectory
        Vector3 camForward = GameObject.FindGameObjectWithTag("ARCamera").transform.forward;
        camForward *= bubbleData.forwardSpeed;
        camForward.y *= Random.Range(bubbleData.minUpSpeed, bubbleData.maxUpSpeed);
        float randYRotOffset = Random.Range(-bubbleData.maxAngleDeviationOnY, bubbleData.maxAngleDeviationOnY);
        trajectory = Quaternion.Euler(0, randYRotOffset, 0) * camForward;

        //Material
        int index = (int)Random.Range(0, bubbleMats.Count - 1);
        this.GetComponent<MeshRenderer>().material = bubbleMats[index];

        //Scale
        float randScale = Random.Range(bubbleData.minScale, bubbleData.maxScale);
        this.transform.localScale = new Vector3(randScale, randScale, randScale);

        //Set Lifetime
        float randLifetime = Random.Range(bubbleData.minLifetime, bubbleData.maxLifetime);
        Invoke("burstBubble", randLifetime);

    }
    
    void Update()
    {
        //Fly forward and slowely down
        trajectory.y -= bubbleData.gravityStrength;
        this.transform.position += trajectory;
    }

    private void OnCollisionExit(Collision collision)
    {
        burstBubble();
    }

    private void burstBubble()
    {
        BubblePopped();
        DestroyImmediate(this.gameObject);
    }

}
