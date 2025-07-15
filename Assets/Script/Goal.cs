using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    public GoalButton GoalButton; // インスペクターでButtonのスクリプトをドラッグして渡す
    public string sceneName; // インスペクターで指定するシーン名（例："GameScene"）
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("sceneNameが指定されていません！");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GoalButton.isTouchingDead)
>>>>>>> origin/main
        {
            ChangeScene();
        }
    }
}

