using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VolumeController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayers = FindObjectsOfType<VideoPlayer>();
        //
    }

    // Update is called once per frame
    void Update()
    {
        videoPlayers = FindObjectsOfType<VideoPlayer>();
    }

    public void ChangeVolume(float amt)
    {
        foreach (var videoPlayer in videoPlayers)
        {
            float new_vol;
            float cur_vol = videoPlayer.GetDirectAudioVolume(0);
            new_vol = Mathf.Clamp(cur_vol + amt, 0, 1);
            videoPlayer.SetDirectAudioVolume(0, new_vol);
        }
    }
}
