using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float lifetime = 5f; // 消えるまでの時間（秒）

    void Start()
    {
            // 指定時間後にこのオブジェクトを削除する
            Destroy(gameObject, lifetime);
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead"))
        {

        }
    }
}
