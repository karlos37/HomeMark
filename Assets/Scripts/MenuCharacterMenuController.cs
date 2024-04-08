using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCharacterMenuController : MonoBehaviour
{
    private CharacterController characterMovement;
    public GameObject reticle;

    public GameObject menu;
    private float menuDistFromUser = 1.0f;
    private bool menuActive = false;

    public GameObject[] buttons;
    private float delay = 0.01f;
    private int buttonIndex = 0;
    private bool isScrolling = false;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterController>();
        menuActive = false;
        menu.SetActive(menuActive);
    }

    // Update is called once per frame
    void Update()
    {
        menu.SetActive(menuActive);
        if (Input.GetButtonDown("js1"))
        {
            Debug.Log("Menu button pressed!");
            buttonIndex = 0;
            // spawn menu in front of user
            Vector3 userPosition = transform.position;
            Vector3 menuPosition = userPosition + transform.forward * menuDistFromUser;

            menu.transform.position = menuPosition;
            menuActive = !menuActive;

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(transform.GetChild(0).GetChild(0).Find("VolUp").gameObject);

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
