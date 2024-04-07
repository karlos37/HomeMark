using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessController : MonoBehaviour
{
    public float defaultBrightness = 1f;

    public Light localLight; // Reference to the light controlled by the local client

    // Start is called before the first frame update
    void Start()
    {
        localLight = FindObjectOfType<Light>();
        if (localLight == null)
        {
            Debug.LogError("No Light component found in the scene!");
        }
        else
        {
            localLight.intensity = defaultBrightness;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBrightness(float amt)
    {
        // Debug.Log("<color=blue>Brightness: </color>" + localLight.intensity);
        localLight.intensity += amt;
    }
}
