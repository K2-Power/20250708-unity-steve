using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float lifetime = 5f; // ������܂ł̎��ԁi�b�j

    void Start()
    {
            // �w�莞�Ԍ�ɂ��̃I�u�W�F�N�g���폜����
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
