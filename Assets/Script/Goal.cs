using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    public GoalButton GoalButton; // インスペクターでButtonのスクリプトをドラッグして渡す
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GoalButton.isTouchingDead)
        {
            ChangeScene();
        }
    }
}

