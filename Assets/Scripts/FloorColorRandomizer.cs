using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColorRandomizer : MonoBehaviour
{
    public Color[] FloorColors;

    void Awake()
    {
        GetComponent<SpriteRenderer>().color = FloorColors[Random.Range(0, FloorColors.Length - 1)];
    }
}
