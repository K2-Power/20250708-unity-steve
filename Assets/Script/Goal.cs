using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{   
    public string sceneName; // インスペクターで指定するシーン名（例："GameScene"）
    public GoalButton GoalButton;
  
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GoalButton.isTouchingDead== true)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

