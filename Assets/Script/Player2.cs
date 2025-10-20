using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float lifetime = 5f; // ������܂ł̎��ԁi�b�j
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("�_�Őݒ�")]
    public float blinkSpeed = 2f;       // �_�ł̑��x
    public float minAlpha = 0f;         // �ŏ������x
    public float maxAlpha = 1f;         // �ő哧���x
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

        // �w�莞�Ԍ�ɂ��̃I�u�W�F�N�g���폜����
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
            // Freeze Position X ������
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
            // �Ă� Freeze Position X ��L����
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
