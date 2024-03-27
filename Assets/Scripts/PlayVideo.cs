using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayer : MonoBehaviour
{
    [SerializeField] GameObject videoPlayer;
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayVideo()
    {
        canvas.enabled = false;
        videoPlayer.SetActive(true);
        VideoPlayer player = videoPlayer.gameObject.GetComponent<VideoPlayer>();
        player.PlayVideo();
    }
}
