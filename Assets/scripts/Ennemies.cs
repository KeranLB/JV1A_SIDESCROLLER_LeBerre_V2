using UnityEngine;

public class Ennemies : MonoBehaviour
{
    private string pointObjectiv;
    
    public float moovSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>().GotShield == true)
        {
            Destroy(gameObject);
        }
    }

// Update is called once per frame
void Update()
    {

    }
}
