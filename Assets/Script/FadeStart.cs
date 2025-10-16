using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class FadeStart : MonoBehaviour
{
    [SerializeField]
    private Fade m_fade = null;

    private void Start()
    {
        m_fade.FadeIn(2.0f);
    }

    private IEnumerator Wait3SecondsAndFadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        m_fade.FadeOut(2.0f);
    }
}
