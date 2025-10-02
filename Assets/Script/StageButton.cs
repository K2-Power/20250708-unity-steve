using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public List<GameObject> ButtonObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DestroyAll();
            Destroy(gameObject);
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
