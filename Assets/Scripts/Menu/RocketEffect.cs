using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEffect : MonoBehaviour
{
    public float Scale;
    float startY;
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(Time.time) * Scale, transform.position.z); ;
    }
}
