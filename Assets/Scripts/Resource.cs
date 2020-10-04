using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceType Type;
    public int Quantity = 3;

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

    public int Collect()
    {
        Quantity--;

        if (Quantity <= 0)
        {
            GetComponent<ToggleResourceSprite>().enabled = true;
            //Destroy(gameObject);
            if (Quantity == 0)
                return 1;
            else
                return 0;
        }

        return 1;
    }
}

public enum ResourceType
{
    Stone = 0, Wood = 1, Alien = 2
}