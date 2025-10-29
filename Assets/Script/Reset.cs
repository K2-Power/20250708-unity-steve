using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; // �V�[���̊Ǘ��ɕK�v

public class Reset : MonoBehaviour
{
    public GameObject ResetText;
    public GameObject RestartText;
    public GameObject player;

    void Start()
    {
        if (ResetText != null)
        {
            ResetText.SetActive(false);
        }
        if (RestartText != null)
        {
            RestartText.SetActive(false);
        }
    }
    // Update�֐��͖��t���[���Ă΂��
    void Update()
    {
        if (player == null)
        {
            ResetGame();
        }
        if (Player.instance.CloneCount <= 0)
        {
            if (GameObject.FindWithTag("ego") == null)
            {
                ResetText.SetActive(true);
            }
        }
        // �uR�v�L�[�������ꂽ�烊�Z�b�g
        if (Input.GetKeyDown("joystick button 3")|| Input.GetKeyDown(KeyCode.R))
        {
        
            ResetGame(); // ���Z�b�g�֐����Ăяo��
        }
    }

    // �Q�[���̃��Z�b�g�����i�V�[���̍ēǂݍ��݁j
    void ResetGame()
    {
        // ���݂̃A�N�e�B�u�ȃV�[�����擾
        Scene currentScene = SceneManager.GetActiveScene();

        // ���̃V�[�����ēǂݍ��݂��ă��Z�b�g
        SceneManager.LoadScene(currentScene.name);
    }
}
