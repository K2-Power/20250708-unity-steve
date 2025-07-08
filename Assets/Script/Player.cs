using UnityEngine;

public class Player : MonoBehaviour
{
        public float moveSpeed = 5f;     // 左右の移動速度
        public float jumpForce = 7f;     // ジャンプの強さ
        public LayerMask groundLayer;    // 地面のレイヤーを指定するためのフィールド
        public Transform groundCheck;    // 地面チェック用の位置（空オブジェクトを設定）
        public GameObject MainPlayer;
        public GameObject ClonePlayer;


    private Rigidbody2D rb;
        private bool isGrounded;

        public GameObject player2Prefab; // Player2のプレハブを指定

    void Start()
    {
        CollisionDisabler.SetActiveCollision(false,MainPlayer,ClonePlayer);
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2Dを取得
    }

        void Update()
        {
            // 左右移動（A：左, D：右）
            float moveX = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveX = 1f;
            }
            rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // 右クリック（マウス右ボタン）で生成
        if (Input.GetMouseButtonDown(1)) // 0=左, 1=右
        {
            if (player2Prefab != null)
            {
                Instantiate(player2Prefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Player2プレハブが設定されていません！");
            }
        }
        // ジャンプ（Spaceキー）
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
        else if (collision.collider.CompareTag("Button"))
        {
            isGrounded = false;
        }
    }
}



