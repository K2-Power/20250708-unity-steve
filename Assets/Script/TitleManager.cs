using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���̊Ǘ��ɕK�v

public class TitleManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown("joystick button 2"))
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }
}
