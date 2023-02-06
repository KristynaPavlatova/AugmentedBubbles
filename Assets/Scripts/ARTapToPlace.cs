using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlace : MonoBehaviour
{
    public GameObject GameObjectToInstantiate;

    private ARRaycastManager _arRaycastManager;
    private Vector2 _touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public UnityEvent OnRingFirstPlaced;
    private bool wasObjectFirstPlaced = false;

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 _touchPosition)
    {
        if(Input.touchCount > 0)
        {
            _touchPosition = Input.GetTouch(0).position;//get the first touch position
            return true;
        }
        _touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 _touchPosition))
            return;

        //shoot raycast
        if(_arRaycastManager.Raycast(_touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            //Event to hide UI
            if (!wasObjectFirstPlaced)
            {
                OnRingFirstPlaced.Invoke();
                wasObjectFirstPlaced = true;
            }

            GameObjectToInstantiate.transform.position = hitPose.position;
        }
    }
}
