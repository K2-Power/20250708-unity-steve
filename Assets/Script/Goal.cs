using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    public Button buttonScript; // �C���X�y�N�^�[��Button�̃X�N���v�g���h���b�O���ēn��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && buttonScript.isTouchingDead)
        {
            SceneManager.LoadScene("GameScene2");
        }
    }
}

