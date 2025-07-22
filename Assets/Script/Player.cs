using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float ShakingSpeed = 0.0f;
    public GameObject player2Prefab; // Player2のプレハブを指定
    public static Player instance;

    void Start()
    {
        instance = this;
        CollisionDisabler.SetActiveCollision(false,MainPlayer,ClonePlayer);
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2Dを取得

    }

        void Update()
        {
        //Gamepad.current?.SetMotorSpeeds(2.0f, 2.0f);
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        // 左右移動（A：左, D：右）
        float moveX = 0f;
            if (hori < 0)
            {
                moveX = -1f;
            }
            else if (hori > 0)
            {
                moveX = 1f;
            }

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        if (CloneCount <= 0)
        {
            Destroy(gameObject);
        }



        // 右クリック（マウス右ボタン）で生成
        if (Input.GetMouseButtonDown(1)) // 0=左, 1=右
        {
            if (CloneCount > 0)
            {
                if (player2Prefab != null)
                {
                    CloneCount--;
                    Instantiate(player2Prefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Player2プレハブが設定されていません！");
                }
            }
        }

        // 右クリック（マウス右ボタン）で生成
        if (Input.GetKeyDown("joystick button 5")) // 0=左, 1=右
        {
            if (CloneCount > 0)
            {
                if (player2Prefab != null)
                {
                    CloneCount--;
                    Instantiate(player2Prefab, transform.position, Quaternion.identity);
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
        }
        


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Dead"))
            {
                Destroy(gameObject); // playerを壊す
            }
        }

    // Groundに触れている間
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            
            isGrounded = true;
        }
        else if (collision.collider.CompareTag("Button")) 
        {
            isGrounded = true;
        }
    }

    // Groundから離れたとき
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
           
            isGrounded = false;
        }
        else if (collision.collider.CompareTag("Button"))
        {
            isGrounded = false;
        }
    }
}



