using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetailRandomizer : MonoBehaviour
{
    public Color[] DetailColors;

    void Awake()
    {
        //Color c = DetailColors[Random.Range(0, DetailColors.Length - 1)];

        foreach(Transform t in transform)
        {
            t.GetComponent<SpriteRenderer>().color = DetailColors[Random.Range(0, DetailColors.Length - 1)];
        }
    }

}
