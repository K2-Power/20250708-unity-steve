using UnityEngine;

public class Seesaw : MonoBehaviour
{
    public float rotateSpeed = 100f; // ��]�X�s�[�h�i�x/�b�j
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.angularVelocity = rotateSpeed; // �����ŉ�]�X�s�[�h��ݒ�
    }
}
