using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{

    public GameObject camera;

    public void IsLocalPlayer()
    {
        GetComponent<CharacterController>().enabled = true;
		GetComponent<CharacterMovement>().enabled = true;
        camera.SetActive(true);

    }
}
