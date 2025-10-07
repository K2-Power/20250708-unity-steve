using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorSpeed = 2f; // �x���g�̑���

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ego"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // �v���C���[�ɉ������̑��x��^����
                rb.linearVelocity = new Vector2(conveyorSpeed, rb.linearVelocity.y);
            }
        }
    }
}
