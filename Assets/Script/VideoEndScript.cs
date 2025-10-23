using UnityEngine;
using UnityEngine.Video;

public class VideoEndScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject Screen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.loopPointReached += screendelete;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void screendelete(VideoPlayer vp)
    {
        Screen.SetActive(false);
        vp.loopPointReached -= screendelete;
    }
}
