using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���̊Ǘ��ɕK�v

public class TitleManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown("joystick button 5"))
        {
            SceneManager.LoadScene("GameScene1");
        }
    }
}
