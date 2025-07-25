using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Deadに触れているかどうかの状態
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
