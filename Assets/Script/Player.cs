using System;
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
    void Start()
    {
        StoreOriginalChildPositions();
        instance = this;
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2Dを取得
        animator = GetComponent<Animator>();
        originalGravity = rb.gravityScale;
        originalMass = rb.mass;

    }

    void Update()
    {
        //Gamepad.current?.SetMotorSpeeds(6.0f, 6.0f);
        if (isGrounded && animator)
        {
            animator.SetBool("jumpFlag",false);
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
            if (animator)
            {
                animator.SetBool("moveFlag", false);
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
                    //SoundManager.instance.PlaySE(0);
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
            isGrounded = true;
        }
        if (collision.collider.CompareTag("Button"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Lift"))
        {
            transform.SetParent(collision.transform);
            isGrounded = true;
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
            isGrounded = true;
        }
        if (collision.collider.CompareTag("Button"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Conveyors"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Lift"))
        {
            transform.SetParent(collision.transform);
            isGrounded = true;
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
        }
        if (collision.gameObject.CompareTag("Conveyors"))
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



