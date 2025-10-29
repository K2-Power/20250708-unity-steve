using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;     // 左右の移動速度
    public float jumpForce = 7f;     // ジャンプの強さ
    public float MinusmoveSpeed= 0.3f;//減らすジャンプ力
    public LayerMask groundLayer;    // 地面のレイヤーを指定するためのフィールド
    public Transform groundCheck;    // 地面チェック用の位置（空オブジェクトを設定）
    public GameObject MainPlayer;
    public GameObject ClonePlayer;
    public int CloneCount = 0;
    private Rigidbody2D rb;
    private Rigidbody2D rb2;
    private bool isGrounded;
    public GameObject player2Prefab; // Player2のプレハブを指定
    public GameObject ShotPoint;
    public GameObject ResetText;
    public static Player instance;
    public GameObject LiftObject;
    public bool autoFlipChildren = true;
    private bool facingRight = true;
    private Animator animator;
    private Transform[] childTransforms;
    private Vector3[] originalChildPositions;
    private float originalGravity;
    private float originalMass;
    private bool onTheLift = false;
    public GameObject[] clonePrefabs;   // クローンのプレハブを複数登録
    private int currentCloneIndex = 0;  // 現在のクローン番号

    void Start()
    {
        StoreOriginalChildPositions();
        instance = this;
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2Dを取得
        rb2 = player2Prefab.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravity = rb.gravityScale;
        originalMass = rb.mass;
        onTheLift = false;
    }

    void Update()
    {
        //if (onTheLift)
        //{
        //    Debug.Log("LIFTON");
        //}
        //else
        //{
        //    Debug.Log("LIFTOFF");
        //}
        Debug.Log("PlayerVeloX" + rb.linearVelocityX.ToString());
        //Gamepad.current?.SetMotorSpeeds(6.0f, 6.0f);
        if (isGrounded && animator)
        {
            animator.SetBool("jumpFlag", false);
        }
        if (!isGrounded && animator)
        {
            animator.SetBool("jumpFlag",true);
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        //Debug.Log(horizontal);
        // 左右移動（A：左, D：右）
        if (Mathf.Abs(horizontal) > 0.01f)
        {
            if (animator)
            {
                animator.SetBool("moveFlag",true);
            }
            Vector3 movement = new Vector3(horizontal * (moveSpeed * Time.deltaTime), 0, 0);
            //transform.Translate(movement);
            //rb.linearVelocityX = movement.x;
            //rb.linearVelocityX = horizontal * (moveSpeed * Time.deltaTime); 
            rb.linearVelocityX = horizontal * moveSpeed;
            //rb.linearVelocity = movement;
            
            // 自動反転が有効な場合
            if (autoFlipChildren)
            {
                // 右向きから左向きに変わった時
                if (horizontal < -0.2f)
                {
                    transform.rotation = Quaternion.Euler(0,180,0);
                }
                // 左向きから右向きに変わった時
                else if (horizontal > 0.2f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);                    
                }
            }
        }
        if (Mathf.Abs(horizontal) == 0.0f)
        {
            rb.linearVelocityX = 0;
            if (animator)
            {
                animator.SetBool("moveFlag", false);
            }
        }



        // 右クリック（マウス右ボタン）で生成
        if (Input.GetMouseButtonDown(1)) // 0=左, 1=右
        {
            SpawnNextClone();
        }

        // 右クリック（マウス右ボタン）で生成
        if (Input.GetKeyDown("joystick button 2")) // 0=左, 1=右
        {
            SpawnNextClone();
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

    void SpawnNextClone()
    {
        if (CloneCount > 0)
        {
            // プレハブが正しく設定されていない場合は警告
            if (clonePrefabs == null || clonePrefabs.Length == 0)
            {
                Debug.LogWarning("クローンのプレハブが設定されていません！");
                return;
            }

            // 使用するプレハブを選択
            GameObject prefab = clonePrefabs[currentCloneIndex];
            currentCloneIndex = (currentCloneIndex + 1) % clonePrefabs.Length; // 次の順番へ

            // 実際に生成
            GameObject clone = Instantiate(prefab, ShotPoint.transform.position, Quaternion.identity);
            CloneCount--;
            moveSpeed -= MinusmoveSpeed;　//ジャンプ力を減らす

            // もしリフト上にいるなら追加の力を加える
            if (onTheLift)
            {
                Rigidbody2D cloneRb = clone.GetComponent<Rigidbody2D>();
                if (cloneRb != null)
                {
                    cloneRb.AddForce(new Vector2(3, 0));
                }
            }

            SoundManager.instance.PlaySE(0);
        }
        else
        {
            Debug.Log("クローンが残っていません！");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.CompareTag("Dead"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("ego"))
        {
            isGrounded = true;
        }
        if (collision.CompareTag("Lift"))
        {
            isGrounded = true;
            onTheLift = false;
            if (LiftObject != null)
            {
                if (Input.GetAxis("Horizontal") == 0.0f)
                {
                    rb.linearVelocityX = 0.0f;
                }
                transform.SetParent(LiftObject.transform);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.CompareTag("ego"))
        {
            isGrounded = true;
        }
        if (collision.CompareTag("Lift"))
        {
            isGrounded = true;
            onTheLift = false;
            if (LiftObject != null)
            {
                if (Input.GetAxis("Horizontal") == 0.0f)
                {
                    rb.linearVelocityX = 0.0f;
                }
                transform.SetParent(LiftObject.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.CompareTag("ego"))
        {
            isGrounded = false;
        }
        if (collision.CompareTag("Lift"))
        {
            isGrounded = false;
            onTheLift = false;
            if (LiftObject != null)
            {
                transform.SetParent(null);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 接触点の法線をチェック（上方向＝地面）
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                }
            }
        }
        if (collision.collider.CompareTag("Lift"))
        {
            transform.SetParent(collision.transform);  // Liftの子にする
        }
        if (collision.collider.CompareTag("Button"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Lift"))
        {
            transform.SetParent(collision.transform);
            isGrounded = true;
            onTheLift = true;
        }
        if (collision.gameObject.CompareTag("Conveyors"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            bool groundedNow = false;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 下方向の接触のみ地面扱い
                if (contact.normal.y > 0.5f)
                {
                    groundedNow = true;
                    break;
                }
            }
            isGrounded = groundedNow;
        }
            if (collision.collider.CompareTag("Button"))
            {
                isGrounded = true;
            }
            if (collision.gameObject.CompareTag("Conveyors"))
            {
                isGrounded = true;
            }
            if (collision.gameObject.CompareTag("ego"))
            {
            bool groundedNow = false;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 下方向の接触のみ地面扱い
                if (contact.normal.y > 0.5f)
                {
                    groundedNow = true;
                    break;
                }
            }
            isGrounded = groundedNow;
        }
            if (collision.gameObject.CompareTag("Lift"))
            {
                transform.SetParent(collision.transform);
                isGrounded = true;
                onTheLift = true;
             bool groundedNow = false;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 下方向の接触のみ地面扱い
                if (contact.normal.y > 0.5f)
                {
                    groundedNow = true;
                    break;
                }
            }
            isGrounded = groundedNow;
            }

            
        }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.collider.CompareTag("Button"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Lift"))
        {
            transform.SetParent(null);
            isGrounded = false;
            onTheLift = false;
        }
        if (collision.gameObject.CompareTag("Conveyors"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("ego"))
        {
            isGrounded = true;
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

    public void EnterWind(float newGravity, float newMass)
    {
        rb.gravityScale = newGravity;
        rb.mass = newMass;
    }
    public void ExitWind()
    {
        rb.gravityScale = originalGravity;
        rb.mass = originalMass;
    }

}