using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class collectible : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<PlayerMovement>().GotShield = true;
        Destroy(gameObject,0.1f);
    }
}
