using UnityEngine;
using UnityEngine.SceneManagement; // シーンの管理に必要

public class TitleManager : MonoBehaviour
{
    public GameObject title;
    public GameObject title1;
    private int counter;
    public int Maxcounter;

    void Start()
    {
         counter = 0;
        title1.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown("joystick button 2")|| Input.GetMouseButtonDown(1))
        {
            
            counter++;
        }
        switch (counter)
        {
            case 1:
                title1.SetActive(false);
                title1.SetActive(true);
                break;
            case 2:
                SceneManager.LoadScene("GameScene1");
                break;
            default:
                break;
        }
    }
}
