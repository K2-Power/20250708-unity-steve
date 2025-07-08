using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Dead‚ÉG‚ê‚Ä‚¢‚é‚©‚Ç‚¤‚©‚Ìó‘Ô
    public bool isTouchingDead { get; private set; } = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            spriteRenderer.enabled = false;
            isTouchingDead = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            spriteRenderer.enabled = true;
            isTouchingDead = false;
        }
    }
}
