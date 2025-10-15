using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class StageButton1 : MonoBehaviour
{
    public List<GameObject> ButtonObject;
    public bool PlayerOREgo = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerOREgo == false)
        {
            if (collision.CompareTag("ego"))
            {
                foreach (GameObject obj in ButtonObject)
                {
                    if (obj != null)
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                foreach (GameObject obj in ButtonObject)
                {
                    if (obj != null)
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PlayerOREgo == false)
        {
            if (collision.CompareTag("ego"))
            {
                foreach (GameObject obj in ButtonObject)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                foreach (GameObject obj in ButtonObject)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }
    }
}
