using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    [SerializeField] GameObject videoPlayer;
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        //Playvideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Playvideo()
    {
        canvas.enabled = false;
        videoPlayer.SetActive(true);
        VideoPlayer player = videoPlayer.gameObject.GetComponent<VideoPlayer>();
        player.PlayVideo();
    }
}
