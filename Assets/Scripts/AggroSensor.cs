using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroSensor : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

        if(pc != null)
        {
            //GetComponent<CircleCollider2D>().enabled = false;
            gameObject.SetActive(false);
            GetComponentInParent<EnemyController>().Aggro(pc.transform);
        }
    }
}
