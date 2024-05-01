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
        if (Input.GetButtonDown("js1") or Input.GetButtonDown("js5"))
        {
            Debug.Log("Menu button pressed!");
            // spawn menu in front of user
            Vector3 userPosition = transform.position;
            Vector3 menuPosition = userPosition + transform.forward * menuDistFromUser;

            menu.transform.position = menuPosition;
            menuActive = !menuActive;

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(transform.Find("HeightOffset").Find("Menu").Find("VolUp").gameObject);
		}
        if (menuActive)
        {
            characterMovement.enabled = false;
            reticle.SetActive(false);

        }
        else
        {
            characterMovement.enabled = true;
			reticle.SetActive(true);
		}
    }
}
