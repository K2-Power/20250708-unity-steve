using UnityEngine;
using UnityEngine.SceneManagement; // シーンの管理に必要

public class TitleManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown("joystick button 2"))
        {
            SceneManager.LoadScene("GameScene1");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("GameScene1");
        }
    }
}
