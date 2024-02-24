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

    public SpriteRenderer hearthA;
    public SpriteRenderer hearthB;
    public SpriteRenderer hearthC;
    public SpriteRenderer shield;

    public float jumpForce;
    public float moovSpeed;

    public Vector3 respawnPoint;

    // private variables
    public int vie;
    public bool IsGrounded;
    public bool IsLeftWalled;
    public bool IsRightWalled;
    public bool GotShield ;
    public bool isWallJumping;
    public bool isWalking;

    public bool blocRight;
    public bool blocLeft;

    public bool kill;

    private float Move;


    void Start()
    {
        vie = 3;
        kill = false;
        isWallJumping = false;
        isWalking = false;
        GotShield = false;
        respawnPoint = transform.position;
        blocLeft = false;
        blocRight = false;
        shield.gameObject.SetActive(false);
        hearthA.gameObject.SetActive(true);
        hearthB.gameObject.SetActive(true);
        hearthC.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemie"))
        {
            if (GetComponent<PlayerMovement>().GotShield == false)
            {
                vie--;
                if (vie < 1)
                {
                    transform.position = new Vector3(124, 100, 0);
                }
                else
                {
                    transform.position = respawnPoint;
                }
            }
            if (GetComponent<PlayerMovement>().GotShield == true)
            {
                kill = true;
            }
        }
        if (collision.CompareTag("Kill"))
        {
            vie--;
            if (vie < 1)
            {
                transform.position = new Vector3(124, 100, 0);
            }
            else
            {
                transform.position = respawnPoint;
            }

        }

        if (collision.CompareTag("Checkpoint"))
        {
            respawnPoint = collision.transform.position;
        }

        else if (collision.CompareTag("wayout"))
        {
            if (kill == true)
            {
                transform.position = new Vector3(100, 100, 0);
            }
        }

        if (collision.CompareTag("hearth"))
        {
            if (vie < 3)
            {
                vie++;
                Destroy(collision.gameObject);
            }
        }
    }

    void Update()
    {
        if (GotShield == true)
        {
            shield.gameObject.SetActive(true);
        }
        if (vie == 3)
        {
            hearthA.gameObject.SetActive(true);
            hearthB.gameObject.SetActive(true);
            hearthC.gameObject.SetActive(true);
        }
        if (vie == 2)
        {
            hearthA.gameObject.SetActive(true);
            hearthB.gameObject.SetActive(true);
            hearthC.gameObject.SetActive(false);
        }
        if (vie == 1)
        {
            hearthA.gameObject.SetActive(true);
            hearthB.gameObject.SetActive(false);
            hearthC.gameObject.SetActive(false);
        }
        if (vie == 0)
        {
            hearthA.gameObject.SetActive(false);
            hearthB.gameObject.SetActive(false);
            hearthC.gameObject.SetActive(false);
        }

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

        if (IsRightWalled == false)
        {
            blocLeft = false;
            blocRight = false;
        }

        if (IsRightWalled == false && isWallJumping == false)
        {
            if (blocRight == false)
            {
                if (Input.GetKey(rightKey))
                {
                    transform.Translate(Vector2.right * moovSpeed * Time.deltaTime);
                    gameObject.GetComponent<Animator>().Play("walk");
                    isWalking = true;
                }
            }

            if (blocRight == false)
            {
                if (Input.GetKey(leftKey))
                {
                    transform.Translate(Vector2.left * moovSpeed * Time.deltaTime);
                    gameObject.GetComponent<Animator>().Play("walk");
                    isWalking = true;
                }
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

        else if (IsRightWalled)
        {
            if (gameObject.transform.localScale == new Vector3(1, 1, 1))
            {
                blocRight = true;
            }

            if (gameObject.transform.localScale == new Vector3(-1, 1, 1))
            {
                blocLeft = true;
            }

            if (blocRight == true)
            {
                if (Input.GetKeyDown(upKey))
                {
                    isWallJumping = true;
                    rgbd.AddForce(Vector2.up * jumpForce * 0.75f);
                    rgbd.AddForce(Vector2.left * jumpForce);
                    gameObject.GetComponent<Animator>().Play("walk");
                }
            }

            if (blocLeft == true)
            {
                if (Input.GetKeyDown(upKey))
                {
                    isWallJumping = true;
                    rgbd.AddForce(Vector2.up * jumpForce * 0.75f);
                    rgbd.AddForce(Vector2.right * jumpForce);
                    gameObject.GetComponent<Animator>().Play("walk");
                }
            }

        }
    }
}
