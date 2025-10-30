using UnityEngine;
using System;
using System.Collections;

public class GameFadeScript : MonoBehaviour
{
    [SerializeField]
    private Fade m_fade = null;
    public float m_fadeTime = 0.0f;

    private void Start()
    {
        m_fade.FadeIn(m_fadeTime);
    }

    private IEnumerator Wait3SecondsAndFadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        m_fade.FadeOut(2.0f);
    }
}
