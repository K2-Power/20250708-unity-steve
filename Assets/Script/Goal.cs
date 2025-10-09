using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite OFFsprite;
    public Sprite ONsprite;
    public string sceneName; // �C���X�y�N�^�[�Ŏw�肷��V�[�����i��F"GameScene"�j
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

