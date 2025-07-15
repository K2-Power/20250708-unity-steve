using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public static Button instance;

    // DeadÇ…êGÇÍÇƒÇ¢ÇÈÇ©Ç«Ç§Ç©ÇÃèÛë‘
    public bool isTouchingDead { get; private set; } = false;

    void Start()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            Vector3 localscale = gameObject.transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
            isTouchingDead = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            Vector3 localscale = gameObject.transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
            isTouchingDead = false;
        }
    }
}
