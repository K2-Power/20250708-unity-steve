using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public string gamescene;
    void Update()
    {
        if (Input.GetKeyDown("joystick button 2") || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(gamescene);
        }
    }
}
