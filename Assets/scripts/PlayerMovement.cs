using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public variable
    public KeyCode leftKey, rightKey, upKey;
    
    public Rigidbody2D rgbd;

    public Transform LeftCheckGrounbed;
    public Transform RightCheckGrounded;

    public Transform TopLeftCheckWalled;
    public Transform BottomLeftCheckWalled;

    public Transform TopRightCheckWalled;
    public Transform BottomRightCheckWalled;
    
    public float jumpForce;
    public float moovSpeed;

    public Vector3 respawnPoint;

    // private variables
    public bool IsGrounded;
    public bool IsLeftWalled;
    public bool IsRightWalled;
    public bool GotShield ;
    public bool isWallJumping;
    public bool isWalking;

    private float Move;


    void Start()
    {
        isWallJumping = false;
        isWalking = false;
        GotShield = false;
        respawnPoint = transform.position;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemie"))
        {
            if (GetComponent<PlayerMovement>().GotShield == false)
            {
                transform.position = respawnPoint;
            }
        }
        if (collision.CompareTag("Kill"))
        {
            transform.position = respawnPoint;
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            respawnPoint = collision.transform.position;
        }
    }

    void Update()
    {
        isWalking = false;
        Move = Input.GetAxis("Horizontal");
        IsGrounded = Physics2D.OverlapArea(LeftCheckGrounbed.position, RightCheckGrounded.position);
        IsLeftWalled = Physics2D.OverlapArea(TopLeftCheckWalled.position, BottomLeftCheckWalled.position);
        IsRightWalled = Physics2D.OverlapArea(TopRightCheckWalled.position, BottomRightCheckWalled.position);
        
        if (Move > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (Move < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (IsLeftWalled == false && isWallJumping == false)
        {
            if (Input.GetKey(leftKey))
            {
                transform.Translate(Vector2.left * moovSpeed * Time.deltaTime);
                gameObject.GetComponent<Animator>().Play("walk");
                isWalking = true;
            }
        }
        if (IsRightWalled == false && isWallJumping == false)
        {
            if (Input.GetKey(rightKey))
            {
                transform.Translate(Vector2.right * moovSpeed * Time.deltaTime);
                gameObject.GetComponent<Animator>().Play("walk");
                isWalking = true;
            }
        }

        if (IsGrounded)
        {
            isWallJumping = false;
            if (Input.GetKeyDown(upKey))
            {
                rgbd.AddForce(Vector2.up * jumpForce);
            }
            if (isWalking == false)
            {
                gameObject.GetComponent<Animator>().Play("IDE");
            }
        }
        else if (IsLeftWalled)
        {
            if (Input.GetKeyDown(upKey))
            {
                isWallJumping = true;
                rgbd.AddForce(Vector2.up * jumpForce * 0.75f);
                rgbd.AddForce(Vector2.right * jumpForce);
                gameObject.GetComponent<Animator>().Play("walk");
            }       
        }
        else if (IsRightWalled)
        {
            if (Input.GetKeyDown(upKey))
            {
                isWallJumping = true;
                rgbd.AddForce(Vector2.up * jumpForce * 0.75f);
                rgbd.AddForce(Vector2.left * jumpForce);
                gameObject.GetComponent<Animator>().Play("walk");
            }
        }
    }
}
