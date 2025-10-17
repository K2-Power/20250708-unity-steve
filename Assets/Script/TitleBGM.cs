using System.Collections;
using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    private AudioSource bgm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        StartCoroutine(wait2second());
    }
    private IEnumerator wait2second()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        bgm.Play();
    }
}
