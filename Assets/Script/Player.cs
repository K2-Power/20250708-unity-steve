using UnityEngine;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;     // 左右の移動速度
    public float jumpForce = 7f;     // ジャンプの強さ
    public LayerMask groundLayer;    // 地面のレイヤーを指定するためのフィールド
    public Transform groundCheck;    // 地面チェック用の位置（空オブジェクトを設定）
    public GameObject MainPlayer;
    public GameObject ClonePlayer;
    public int CloneCount = 0;
    private Rigidbody2D rb;
    private bool isGrounded;
    public GameObject player2Prefab; // Player2のプレハブを指定
    public GameObject ShotPoint;
    public GameObject ResetText;
    public static Player instance;
    public bool autoFlipChildren = true;
    private bool facingRight = true;
    private Transform[] childTransforms;
    private Vector3[] originalChildPositions;

    void Start()
    {
        StoreOriginalChildPositions();
        instance = this;
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2Dを取得
        

    }

    void Update()
    {
        //Gamepad.current?.SetMotorSpeeds(2.0f, 2.0f);
        float horizontal = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        // 左右移動（A：左, D：右）
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            Vector3 movement = new Vector3(horizontal * (moveSpeed * Time.deltaTime), 0, 0);
            //transform.Translate(movement);
            //rb.linearVelocityX = movement.x;
            rb.linearVelocityX = horizontal * moveSpeed;

            // 自動反転が有効な場合
            if (autoFlipChildren)
            {
                // 右向きから左向きに変わった時
                if (horizontal < 0 && facingRight)
                {
                    FlipChildren();
                    facingRight = false;
                }
                // 左向きから右向きに変わった時
                else if (horizontal > 0 && !facingRight)
                {
                    FlipChildren();
                    facingRight = true;
                }
            }
        }



        // 右クリック（マウス右ボタン）で生成
        if (Input.GetMouseButtonDown(1)) // 0=左, 1=右
        {
            if (CloneCount > 0)
            {
                if (player2Prefab != null)
                {
                    CloneCount--;
                    Instantiate(player2Prefab, ShotPoint.transform.position, Quaternion.identity);
                    SoundManager.instance.PlaySE(0);
                }
                else
                {
                    Debug.LogWarning("Player2プレハブが設定されていません！");
                }
            }
        }

        // 右クリック（マウス右ボタン）で生成
        if (Input.GetKeyDown("joystick button 2")) // 0=左, 1=右
        {
            if (CloneCount > 0)
            {
                if (player2Prefab != null)
                {
                    CloneCount--;
                    Instantiate(player2Prefab, ShotPoint.transform.position, Quaternion.identity);
                    SoundManager.instance.PlaySE(0);
                }
                else
                {
                    Debug.LogWarning("Player2プレハブが設定されていません！");
                }
            }
        }
        // ジャンプ（Spaceキー）
        if (Input.GetKeyDown("joystick button 0") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }


    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            ResetText.SetActive(true);
            Destroy(gameObject); // playerを壊す
        }
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Groundに触れている間
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Groundから離れたとき
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Button"))
        {
            isGrounded = true;
        }
        else if (collision.collider.CompareTag("ego"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Button"))
        {
            isGrounded = true;
        }
        else if (collision.collider.CompareTag("ego"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Button"))
        {
            isGrounded = false;
        }
        else if (collision.collider.CompareTag("ego"))
        {
            isGrounded = false;
        }
    }


    void StoreOriginalChildPositions()
    {
        int childCount = transform.childCount;
        childTransforms = new Transform[childCount];
        originalChildPositions = new Vector3[childCount];

        for (int i = 0; i < childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
            originalChildPositions[i] = childTransforms[i].localPosition;
        }
    }

    void FlipChildren()
    {
        for (int i = 0; i < childTransforms.Length; i++)
        {
            if (childTransforms[i] != null)
            {
                Vector3 flippedPosition = childTransforms[i].localPosition;
                Quaternion flippedRotation = childTransforms[i].rotation;
                flippedRotation.z = -flippedRotation.z;
                flippedPosition.x = -flippedPosition.x;
                childTransforms[i].localPosition = flippedPosition;
                childTransforms[i].rotation = flippedRotation;
            }
        }
    }


}



