using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BubbleData", order = 1)]
public class ScriptableBubbleData : ScriptableObject
{
    //Max angle deviation on Y axis
    [Tooltip("Give the maximal possible angle to rotate the forward vector of a new bubble by around the Y axis.")]
    public float maxAngleDeviationOnY = 45.0f;
    [Tooltip("Make the bubble fly in curve along the Y axis.")]
    public float minUpSpeed = 0.01f;
    public float maxUpSpeed = 0.05f;
    [Space(10)]
    public float forwardSpeed = 0.03f;
    [Space(10)]
    public float minLifetime = 3.0f;
    public float maxLifetime = 15.0f;
    [Space(10)]
    public float gravityStrength = 0.0001f;
    [Space(10)]
    public string pathToAllMaterials = "Materials/";
    [Space(10)]
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
}
