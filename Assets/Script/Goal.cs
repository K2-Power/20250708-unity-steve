using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    public Button buttonScript; // インスペクターでButtonのスクリプトをドラッグして渡す
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && buttonScript.isTouchingDead)
        {
            SceneManager.LoadScene("GameScene2");
        }
    }
}

