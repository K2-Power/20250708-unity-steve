using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���̊Ǘ��ɕK�v

public class TitleManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("GameScene1");
        }
    }
}
