using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���̊Ǘ��ɕK�v

public class Reset : MonoBehaviour
{
    // Update�֐��͖��t���[���Ă΂��
    void Update()
    {
        // �uR�v�L�[�������ꂽ�烊�Z�b�g
        if (Input.GetKeyDown("joystick button 3"))
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
