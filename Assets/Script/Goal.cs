using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
<<<<<<< HEAD
    public Button buttonScript; // �C���X�y�N�^�[��Button�̃X�N���v�g���h���b�O���ēn��
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
    public GoalButton GoalButton; // �C���X�y�N�^�[��Button�̃X�N���v�g���h���b�O���ēn��
    public string sceneName; // �C���X�y�N�^�[�Ŏw�肷��V�[�����i��F"GameScene"�j
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("sceneName���w�肳��Ă��܂���I");
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

