using UnityEngine;
using UnityEngine.SceneManagement; // シーンの管理に必要

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
