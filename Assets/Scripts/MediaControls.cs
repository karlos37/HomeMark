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
        videoPlayer = null;
    }

    public void PlayVideo()
    {
		if (videoPlayerObject.GetComponents<VideoPlayer>()[0].enabled)
		{
			videoPlayer = videoPlayerObject.GetComponents<VideoPlayer>()[0];
		}
		else
		{
			videoPlayer = videoPlayerObject.GetComponents<VideoPlayer>()[1];
		}
		videoPlayer.Play();
    }

    public void PauseVideo()
    {
		if (videoPlayerObject.GetComponents<VideoPlayer>()[0].enabled)
		{
			Debug.Log("here");
			videoPlayer = videoPlayerObject.GetComponents<VideoPlayer>()[0];
		}
		else
		{
			Debug.Log("there");
			videoPlayer = videoPlayerObject.GetComponents<VideoPlayer>()[1];
		}
		videoPlayer.Pause();
    }
}
