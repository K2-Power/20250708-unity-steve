using UnityEngine;
using UnityEngine.Video;

public class VideoEndScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject Screen;
    public static VideoEndScript instance;
    [SerializeField]
    private Fade m_fade = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        videoPlayer.loopPointReached += screendelete;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void screendelete(VideoPlayer vp)
    {
        Screen.SetActive(false);
        m_fade.FadeIn(2.0f);
        vp.loopPointReached -= screendelete;
    }
}
