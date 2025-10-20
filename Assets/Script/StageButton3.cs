using UnityEngine;

public class StageButton3 : MonoBehaviour
{
    public LiftScript LiftScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LiftScript.SetMovementEnabled(false);
    }

    // Update is called once per frame
   Å@private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LiftScript.SetMovementEnabled(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LiftScript.SetMovementEnabled(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LiftScript.SetMovementEnabled(false);
        }
    }
}
