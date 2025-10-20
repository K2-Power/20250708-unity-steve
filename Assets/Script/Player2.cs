using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float lifetime = 5f; // 消えるまでの時間（秒）
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("点滅設定")]
    public float blinkSpeed = 2f;       // 点滅の速度
    public float minAlpha = 0f;         // 最小透明度
    public float maxAlpha = 1f;         // 最大透明度
    public float blinkStartTime = 2f;
    private Rigidbody2D rb;

    private Coroutine blinkCoroutine;
    private Color originalColor;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();

        // 指定時間後にこのオブジェクトを削除する
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= blinkStartTime)
        {
            float alpha = Mathf.Lerp(minAlpha, maxAlpha,
                         (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f);

            originalColor.a = alpha;
        }
        spriteRenderer.color = originalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Conveyors"))
        {
            // Freeze Position X を解除
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (collision.CompareTag("Lift"))
        {
            if (LiftScript.instance.egofleezeX)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.SetParent(collision.transform);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Conveyors"))
        {
            // 再び Freeze Position X を有効化
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        if (collision.gameObject.CompareTag("Lift"))
        {
            if (LiftScript.instance.egofleezeX)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.SetParent(null);
        }
    }

}
