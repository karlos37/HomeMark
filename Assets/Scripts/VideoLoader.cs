using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using UnityEngine.Networking;

public class VideoLoader : MonoBehaviour
{
    public string folderPath = "Assets/Videos"; // Path to the folder containing videos

    private string selectedMovie;
    void Start()
    {
        LoadVideos();
    }

    void LoadVideos()
    {
        string[] videoFiles = Directory.GetFiles(folderPath, "*.mp4"); // Get all .mp4 files in the folder

        foreach (string filePath in videoFiles)
        {
            StartCoroutine(LoadAndPlayVideo(filePath));
        }
    }

    IEnumerator LoadAndPlayVideo(string filePath)
    {
        VideoPlayer videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;

        VideoClip videoClip = null;

        // Create a new video clip and load it from the file path
        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError|| www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                videoPlayer.url = folderPath + selectedMovie;
            }
        }

        if (videoClip != null)
        {
            videoPlayer.clip = videoClip;
            videoPlayer.Prepare();

            // Wait until video is prepared before playing
            while (!videoPlayer.isPrepared)
            {
                yield return null;
            }

            // Assign the video clip to the VideoPlayer component and play it
            videoPlayer.targetCamera = Camera.main;
            videoPlayer.Play();

            // Wait until the video is finished playing
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }

            // Destroy the VideoPlayer component after playing
            Destroy(videoPlayer);
        }
    }
}
