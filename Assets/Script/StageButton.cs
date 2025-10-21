using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public List<GameObject> ButtonObject;
    public LiftScript liftScript;
    public bool Liftonflag = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (liftScript != null)
        {
            liftScript.SetMovementEnabled(false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DestroyAll();
            Destroy(gameObject);
            if (liftScript != null && Liftonflag == true)
            {
                liftScript.SetMovementEnabled(true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyAll();
        }
        if (liftScript != null && Liftonflag == true)
        {
            liftScript.SetMovementEnabled(true);
        }
    }
    public void DestroyAll()
    {
        foreach (GameObject obj in ButtonObject)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        // リスト自体をクリア
        ButtonObject.Clear();
    }
}
