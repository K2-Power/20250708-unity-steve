using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; // �V�[���̊Ǘ��ɕK�v

public class Reset : MonoBehaviour
{
    public GameObject ResetText;

    void Start()
    {
        ResetText.SetActive(false);
    }
    // Update�֐��͖��t���[���Ă΂��
    void Update()
    {
        if (Player.instance.CloneCount <= 0)
        {
            ResetText.SetActive(true);
        }
        // �uR�v�L�[�������ꂽ�烊�Z�b�g
        if (Input.GetKeyDown("joystick button 3"))
        {
            ResetGame(); // ���Z�b�g�֐����Ăяo��
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
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
