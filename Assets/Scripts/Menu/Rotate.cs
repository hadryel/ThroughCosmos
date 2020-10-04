using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed = 1f;

    void Start()
    {

    }


    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * Speed, Space.World);
    }
}
