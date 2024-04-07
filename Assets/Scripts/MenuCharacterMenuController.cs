using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterMenuController : MonoBehaviour
{
    private CharacterMovement characterMovement;

    private GameObject menu;
    private float menuDistFromUser = 2.0f;
    private bool menuActive = false;

    public GameObject[] buttons;
    private float delay = 0.01f;
    private int buttonIndex = 0;
    private bool isScrolling = false;

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
            buttonIndex = 0;
            // spawn menu in front of user
            Vector3 userPosition = transform.position;
            Vector3 menuPosition = userPosition + transform.forward * menuDistFromUser;

            menu.transform.position = menuPosition;
            menuActive = !menuActive;

        }
        if (menuActive)
        {
            characterMovement.enabled = false;

            if (!isScrolling)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    StartCoroutine(ScrollUp(delay));
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartCoroutine(ScrollDown(delay));
                }
            }
            // Selection; uses the B button for this
            // TEST
            // if (Input.GetButtonDown("js10"))
            // PROD
            if (Input.GetKeyDown(KeyCode.C))
            {
                ClickSelectedButton();
            }

        }
        else
        {
            characterMovement.enabled = true;
        }
    }

    IEnumerator ScrollUp(float delay)
    {
        isScrolling = true;
        yield return new WaitForSeconds(delay);

        buttonIndex = (buttonIndex - 1 + buttons.Length) % buttons.Length;
        SelectButton(buttonIndex);

        isScrolling = false;
    }

    IEnumerator ScrollDown(float delay)
    {
        isScrolling = true;
        yield return new WaitForSeconds(delay);

        buttonIndex = (buttonIndex + 1) % buttons.Length;
        SelectButton(buttonIndex);

        isScrolling = false;
    }

    void SelectButton(int index)
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        buttons[index].GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    void ClickSelectedButton()
    {
        buttons[buttonIndex].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }
}
