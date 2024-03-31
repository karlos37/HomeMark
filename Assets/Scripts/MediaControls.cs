using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
//using Google.XR;

public class MediaControls : MonoBehaviour
{
    [SerializeField] GameObject videoPlayerObject;
    [SerializeField] Slider slider;
    private UnityEngine.Video.VideoPlayer videoPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = videoPlayerObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        //// Check for input from Google XR Cardboard SDK (e.g., gaze input)
        //if (Google.XR.Cardboard.Api.IsTriggerPressed)
        //{
        //    // Check if the gaze intersects with the slider
        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position, transform.forward, out hit))
        //    {
        //        if (hit.collider.gameObject == slider.gameObject)
        //        {
        //            // Update the slider value based on gaze input
        //            float gazeInput = CalculateGazeInput(hit.point);
        //            slider.value = gazeInput;
        //        }
        //    }
        //}
    }

    public void PlayVideo()
    {
        print("Play video ");
         videoPlayer.Play();
    }

    public void PauseVideo()
    {
        print("Pause video ");
        videoPlayer.Pause();
    }

    public void ChangeVolume()
    {
        print("Change volume to " + slider.value);
         videoPlayer.SetDirectAudioVolume(0, slider.value);
    }

    public void RestartVideo()
    {
        print("Restart video");
        videoPlayer.Stop();
         videoPlayer.Play();
    }


    private float CalculateGazeInput(Vector3 hitPoint)
    {
        // Calculate the normalized gaze input based on the hit point on the slider
        // You may need to adjust this calculation based on your slider's orientation and layout
        Vector3 localHitPoint = slider.transform.InverseTransformPoint(hitPoint);
        float normalizedInput = Mathf.Clamp01((localHitPoint.x - slider.minValue) / (slider.maxValue - slider.minValue));
        return normalizedInput;
    }
}
