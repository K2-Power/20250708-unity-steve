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


    private Coroutine blinkCoroutine;
    private Color originalColor;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
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
        if (collision.CompareTag("Dead"))
        {

        }
    }
   

    
}
