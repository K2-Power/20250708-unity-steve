using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("joystick button 2"))
        {
            SceneManager.LoadScene("TitleScene");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
