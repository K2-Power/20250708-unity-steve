using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{

    
    public string sceneName; // インスペクターで指定するシーン名（例："GameScene"）
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("sceneNameが指定されていません！");
        }
    }

    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Button.instance.isTouchingDead == true)
        {
            ChangeScene();
        }
    }
}

