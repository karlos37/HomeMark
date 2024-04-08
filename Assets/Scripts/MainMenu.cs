using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public GameObject CreateScreen;
	public GameObject JoinScreen;
	public GameObject Character;

	// Start is called before the first frame update
	void Start()
    {
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("IntroPanel").Find("Create Room Button").gameObject);

		Character.GetComponent<CharacterController>().enabled = false;
		Character.GetComponent<CharacterMovement>().enabled = false;
	}

	public void goToCreateMenu()
	{
		CreateScreen.SetActive(true);
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(CreateScreen.transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
		gameObject.SetActive(false);
	}

	public void goToJoinMenu()
	{
		JoinScreen.SetActive(true);
		gameObject.SetActive(false);
	}

}
