
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    public static Button instance;
    private EnemyScript Enemyobj = null;

    // DeadÇ…êGÇÍÇƒÇ¢ÇÈÇ©Ç«Ç§Ç©ÇÃèÛë‘
    public bool isTouchingDead = false;

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
            Enemyobj = other.gameObject.GetComponent<EnemyScript>();
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
            Enemyobj = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            bool isMoving = Enemyobj.IsMoving();
            if (!isMoving)
            {
                Debug.Log("Ç”ÇÁÇìÇàÇãÇäÇ”Ç•ÇµÇ§ÇÜÇÜÇàÇì");
                isTouchingDead = true;
            }
        }
    }
}
