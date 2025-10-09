using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    private bool canTeleport = true;
    [SerializeField] private ParticleSystem PortalEffects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTeleport && collision.CompareTag("Player"))
        {
            StartCoroutine(Teleport(collision));
        }
    }

    private System.Collections.IEnumerator Teleport(Collider2D player)
    {
        canTeleport = false;

        player.transform.position = destination.position;
        if (PortalEffects != null)
        {
            PortalEffects.Play();
        }

        // 0.5�b�Ԃ͍ăe���|�[�g�s��
        yield return new WaitForSeconds(0.5f);
        canTeleport = true;
    }
}

