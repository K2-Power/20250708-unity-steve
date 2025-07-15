using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    public Button buttonScript; // インスペクターでButtonのスクリプトをドラッグして渡す
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Button.instance.isTouchingDead == true)
        {
            animator.SetBool("DoorBool", true);
        }
        else
        {
            animator.SetBool("DoorBool", false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Button.instance.isTouchingDead == true)
        {
            SceneManager.LoadScene("GameScene2");
        }
    }
}

