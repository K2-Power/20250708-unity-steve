using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    private bool canTeleport = true;

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

        // 0.5秒間は再テレポート不可
        yield return new WaitForSeconds(0.5f);
        canTeleport = true;
    }
}

