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


    private Coroutine blinkCoroutine;
    private Color originalColor;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
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
        if (collision.CompareTag("Dead"))
        {

        }
    }
   

    
}
