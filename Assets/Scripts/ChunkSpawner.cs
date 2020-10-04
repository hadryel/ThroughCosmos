using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject[] FloorDetails;

    //Resources 
    public GameObject[] Stones;
    public GameObject[] Wood;
    public GameObject[] Aliens;
    void Awake()
    {
        foreach(Transform t in transform)
        {
            GameObject.Instantiate(FloorDetails[Random.Range(0, FloorDetails.Length)], t);
        }
    }
}
