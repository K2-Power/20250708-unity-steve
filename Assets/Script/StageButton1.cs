using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class StageButton1 : MonoBehaviour
{
    public List<GameObject> ButtonObject;
    public bool PlayerOREgo = false;
    public bool ALL = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (PlayerOREgo == false || ALL == true)
        {
            if (collision.collider.CompareTag("ego"))
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
        if (PlayerOREgo == true || ALL == true)
        {
            if (collision.collider.CompareTag("Player"))
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (PlayerOREgo == false || ALL == true)
        {
            if (collision.collider.CompareTag("ego"))
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
        if (PlayerOREgo == true || ALL == true)
        {
            if (collision.collider.CompareTag("Player"))
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
