using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform Source;
    public Vector2 Direction;
    public float Speed = 9f;
    public float Damage = 1f;
    public float LifeSpan = 2f;
    void Start()
    {

    }

    void Update()
    {
        if (LifeSpan <= 0)
            Destroy(gameObject);

        transform.position += Time.deltaTime * (Vector3)Direction.normalized * Speed;
        LifeSpan -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if(enemy != null)
        {
            enemy.Damage(Damage, Source);
        }

        Destroy(gameObject);
    }
}
