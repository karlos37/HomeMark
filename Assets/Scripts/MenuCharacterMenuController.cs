using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterMenuController : MonoBehaviour
{
    private GameObject menu;
    private CharacterMovement characterMovement;
    private float menuDistFromUser = 2.0f;
    private bool menuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        menu = transform.Find("XRCardboardRig/HeightOffset/Menu").gameObject;
        characterMovement = GetComponentInChildren<CharacterMovement>();
        menuActive = false;
        menu.SetActive(menuActive);
    }

    // Update is called once per frame
    void Update()
    {
        menu.SetActive(menuActive);
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Menu button pressed!");
            // spawn menu in front of user
            Vector3 userPosition = transform.position;
            Vector3 menuPosition = userPosition + transform.forward * menuDistFromUser;

            menu.transform.position = menuPosition;
            menuActive = !menuActive;

        }
        if (menuActive)
        {
            characterMovement.enabled = false;
        }
        else
        {
            characterMovement.enabled = true;
        }
    }
}
