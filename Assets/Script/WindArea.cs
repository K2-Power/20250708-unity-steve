using UnityEngine;

public class WindArea : MonoBehaviour
{
    [Header("•—‚Ì•ûŒü‚Æ‹­‚³")]
    public Vector2 windForce = new Vector2(2f, 0f);

    [Header("•—ƒGƒŠƒA‚Ì•¨—’²®")]
    public float windGravity = 0.2f;
    public float windMass = 0.5f;

    private Player playerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.EnterWind(windGravity, windMass);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerScript != null)
        {
            playerScript.ExitWind();
            playerScript = null;
        }
    }

    private void FixedUpdate()
    {
        if (playerScript != null)
        {
            Rigidbody2D rb = playerScript.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(windForce, ForceMode2D.Force);
            }
        }
    }

}
