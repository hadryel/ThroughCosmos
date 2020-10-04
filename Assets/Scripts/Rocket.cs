using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void OnMouseEnter()
    {
        InputHandler.LastTarget = gameObject;
    }

    public void OnMouseExit()
    {
        if (InputHandler.LastTarget == gameObject)
            InputHandler.LastTarget = null;
    }
}
