using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{   
    public string sceneName; // �C���X�y�N�^�[�Ŏw�肷��V�[�����i��F"GameScene"�j
   
  
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Button.instance.isTouchingDead == true)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

