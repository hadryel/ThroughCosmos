using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPosition = Target.transform.TransformPoint(new Vector3(0, 0, -10));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
