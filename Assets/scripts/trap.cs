using UnityEngine;

public class trap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>().GotShield == false)
        {
            Destroy(col.gameObject);
        }

    }
}