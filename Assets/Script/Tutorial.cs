using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("joystick button 2"))
        {
            SceneManager.LoadScene("GameScene1");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("GameScene1");
        }
    }
}
