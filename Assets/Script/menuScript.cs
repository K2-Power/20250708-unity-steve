using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class menuScript : MonoBehaviour
{
    public bool menuopen = false;
    public UnityEngine.UI.Button button;
    public UnityEngine.UI.Button button1;
    public GameObject menuimage;
    public GameObject menubutton;
    public GameObject menubutton1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuopen = false;
        menuimage.SetActive(false);
        menubutton.SetActive(false);
        menubutton1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.Escape))
        {
            button.Select();
            menuopen = !menuopen;
        }
        if (menuopen)
        {
            Time.timeScale = 0.0f;
        }
        else if (!menuopen)
        {
            Time.timeScale = 1.0f;
        }
        menuimage.SetActive(menuopen);
        menubutton.SetActive(menuopen);
        menubutton1.SetActive(menuopen);
    }
}
