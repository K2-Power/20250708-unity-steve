using UnityEngine;

public class StageButton2 : MonoBehaviour
{
    public GameObject ButtonObject2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ego"))
        {
            Destroy(ButtonObject2);
            Destroy(gameObject);
        }
    }
}
