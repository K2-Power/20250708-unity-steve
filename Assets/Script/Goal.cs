using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public GameObject gameobject;
    public GameObject gameobject2;
    public string sceneName; // �C���X�y�N�^�[�Ŏw�肷��V�[�����i��F"GameScene"�j
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.gray;
    }
    void Update()
    {
        if (Button.instance.isTouchingDead == true)
        {
            spriteRenderer.color = originalColor;
        }
        else
        {
            spriteRenderer.color = Color.gray;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Button.instance.isTouchingDead == true)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

