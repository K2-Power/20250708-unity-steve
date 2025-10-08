using UnityEngine;

public class Seesaw : MonoBehaviour
{
    public float rotateSpeed = 100f; // 回転スピード（度/秒）
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.angularVelocity = rotateSpeed; // ここで回転スピードを設定
    }
}
