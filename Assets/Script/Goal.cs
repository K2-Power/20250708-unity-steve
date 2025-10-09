using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite OFFsprite;
    public Sprite ONsprite;
    public string sceneName; // インスペクターで指定するシーン名（例："GameScene"）
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = OFFsprite;
    }
    void Update()
    {
        if (Button.instance.isTouchingDead == true)
        {
            spriteRenderer.sprite = ONsprite;
        }
        else
        {
            spriteRenderer.sprite = OFFsprite;
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

