using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("joystick button 2")|| Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
