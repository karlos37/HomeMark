using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	[SerializeField] private XRCardboardInputModule vrInputModule;
	[SerializeField] private StandaloneInputModuleCopy standardInputModuleCopy;
	[SerializeField] private GameObject eventSystemObject;

	public GameObject CreateScreen;
	public GameObject JoinScreen;

	// Start is called before the first frame update
	void Start()
    {
		eventSystemObject.SetActive(false);
		eventSystemObject.SetActive(true);

		standardInputModuleCopy.enabled = true;
		vrInputModule.enabled = false;

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("IntroPanel").Find("Create Room Button").gameObject);
	}

    // Update is called once per frame
    void Update()
    {
		if (vrInputModule.enabled)
		{
			eventSystemObject.SetActive(false);
			eventSystemObject.SetActive(true);

			standardInputModuleCopy.enabled = true;
			vrInputModule.enabled = false;

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(transform.Find("IntroPanel").Find("Create Room Button").gameObject);
		}
		navigateMainMenu();
	}

	void navigateMainMenu()
	{
		if (Input.GetButtonDown("js1"))
		{
			if (EventSystem.current.currentSelectedGameObject.name == "Join Room Button")
			{
                //SceneManager.LoadScene("JoinMenu");
                JoinScreen.SetActive(true);
                gameObject.SetActive(false); //disable main menu screen

            }
			else if (EventSystem.current.currentSelectedGameObject.name == "Create Room Button")
			{
                //SceneManager.LoadScene("CreateMenu");
                CreateScreen.SetActive(true);
                gameObject.SetActive(false); // disable main menu screen
            }
		}
	}

}