using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void InputAction();

    public Vector2 Direction;
    public Vector2 TargetPosition;

    public InputAction FireAction;
    public InputAction TargetAction;

    public static GameObject LastTarget;
    void Start()
    {
    }

    private void OnDisable()
    {
        Direction = Vector2.zero;
    }

    void Update()
    {
        GetDirectionInput();
        GetMouseInput();
    }

    public void GetDirectionInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector3(horizontal, vertical).normalized;

        Direction = direction;
    }

    public void GetMouseInput()
    {
        TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1"))
        {
            if (LastTarget != null && TargetAction != null)
            {
                TargetAction();
                return;
            }

            if (FireAction != null)
                FireAction();
        }
    }
}
