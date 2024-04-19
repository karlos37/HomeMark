using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
//using Google.XR;

public class MediaControls : MonoBehaviour
{
    [SerializeField] GameObject videoPlayerObject;
    private VideoPlayer videoPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
        videoPlayer.Pause();
    }

    public void PlayVideo()
    {
         videoPlayer.Play();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }
}
