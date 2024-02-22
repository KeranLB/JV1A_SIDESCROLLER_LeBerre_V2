using UnityEngine;

public class Ennemies : MonoBehaviour
{   
    public float moovSpeed;

    public Transform LeftPoint;
    public Transform RightPoint;

    private float LeftPointPos;
    private float RightPointPos;
    private float posx;

    private bool goLeft;
    private bool goRight;

    // Start is called before the first frame update
    void Start()
    {
        goLeft = true;
        goRight = false;

        posx = transform.position.x;
        LeftPointPos = LeftPoint.transform.position.x;
        RightPointPos = RightPoint.transform.position.x;
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
        posx = transform.position.x;

        if (posx < LeftPointPos)
        {
            goLeft = false;
            goRight = true;
        }
        if (posx > RightPointPos)
        {
            goLeft = true;
            goRight = false;
        }

        if (goLeft)
        {
            transform.Translate(Vector2.left * moovSpeed * Time.deltaTime);
        }

        if (goRight)
        {
            transform.Translate(Vector2.right * moovSpeed * Time.deltaTime);
        }
    }
}
