using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleResourceSprite : MonoBehaviour
{
    public GameObject[] Deactivate;
    public GameObject[] Activate;
    private void OnEnable()
    {
        foreach (var obj in Deactivate)
        {
            obj.SetActive(false);
        }

        foreach (var obj in Activate)
        {
            obj.SetActive(true);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
