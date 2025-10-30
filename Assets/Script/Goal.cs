using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
public class Goal : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite OFFsprite;
    public Sprite ONsprite;
    public string sceneName; // インスペクターで指定するシーン名（例："GameScene"）
    public Fade m_fade;
    public Player player;
    public float m_fadeTime = 0.0f;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Button.instance.isTouchingDead == true)
        {
            player.Cannotmove();
            m_fade.FadeOut(m_fadeTime);
            StartCoroutine(Wait3SecondsAndFadeOut());
        }
    }
    private IEnumerator Wait3SecondsAndFadeOut()
    {
        yield return new WaitForSeconds(m_fadeTime + 0.2f);
        SceneManager.LoadScene(sceneName);
    }
}

