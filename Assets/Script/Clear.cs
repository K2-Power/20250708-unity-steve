using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public string SceneName;
    void Update()
    {
        if (Input.GetKeyDown("joystick button 2")|| Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
