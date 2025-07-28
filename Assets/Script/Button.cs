using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public static Button instance;   

    // Dead�ɐG��Ă��邩�ǂ����̏��
    public bool isTouchingDead { get; private set; } = false;

    void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            Vector3 localscale = gameObject.transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            Vector3 localscale = gameObject.transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
            isTouchingDead = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            bool isMoving = EnemyScript.instance.IsMoving();
            if (!isMoving)
            {
                Debug.Log("Enemy�̓������~�܂�܂���!");
                isTouchingDead = true;
            }
        }
    }
}
