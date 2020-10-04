using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Life = 3f;
    public float MaximumLife = 3f;

    private void Start()
    {
        Life = MaximumLife;
    }

    public bool IsDamaged()
    {
        return Life < MaximumLife;
    }
}
