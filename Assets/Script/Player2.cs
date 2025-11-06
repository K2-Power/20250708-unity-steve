using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float lifetime = 5f; // ������܂ł̎��ԁi�b�j
    public Collider2D Player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("�_�Őݒ�")]
    public float blinkSpeed = 2f;       // �_�ł̑��x
    public float minAlpha = 0f;         // �ŏ������x
    public float maxAlpha = 1f;         // �ő哧���x
    public float blinkStartTime = 2f;
    private Rigidbody2D rb;

    private Coroutine blinkCoroutine;
    private Color originalColor;
    public static Player2 instance;

    void Start()
    {
        instance = this;
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
        Collider2D myCollider = rb.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(myCollider,Player,true);
        // �w�莞�Ԍ�ɂ��̃I�u�W�F�N�g���폜����
        Destroy(gameObject, lifetime);
        // Player��Collider�擾
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Collider2D playerCol = player.GetComponent<Collider2D>();
            Collider2D myCol = GetComponent<Collider2D>();

            // Player�Ƃ̏Փ˂𖳎�
            Physics2D.IgnoreCollision(myCol, playerCol, true);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Conveyors"))
        {
            // Freeze Position X ������
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (collision.gameObject.CompareTag("Lift"))
        {
            if (LiftScript.instance.egofleezeX)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.SetParent(collision.transform);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Conveyors"))
        {
            // Freeze Position X ������
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (collision.gameObject.CompareTag("Lift"))
        {
            if (LiftScript.instance.egofleezeX)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.SetParent(collision.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Lift"))
        {
            transform.SetParent(collision.transform);
        }
    }

}
