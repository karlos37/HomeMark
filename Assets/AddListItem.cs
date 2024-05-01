using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System;

public class AddListItem : MonoBehaviour
{
    public string folderPath; // Path to the folder containing videos
    public GameObject moviesParent;
    public GameObject movieItemPrefab;
    public CreateMenu createMenu;
    public Button backButton;
    
    private string[] _movies;
    private Button[] _buttons;
    //private int _selectedButtonIndex;
    // Start is called before the first frame update
    void Start()
    {
		folderPath = "/storage/emulated/0/Android/data/com.DefaultCompany.HomeMark/files/";
        _movies = new string[2];
        _movies[0] = "/storage/emulated/0/Android/data/com.DefaultCompany.HomeMark/files/CINEVR - Movie Theater on Demand in VR.mp4";
		_movies[1] = "/storage/emulated/0/Android/data/com.DefaultCompany.HomeMark/files/Oggy and the Cockroaches - Sport Fans (s04e26).mp4";
        if (_movies.Length == 0)
        {
            Debug.LogError("No Movies found in the folder path");
        }
        string[] filenames = new string[_movies.Length];

        for(int i =0; i < _movies.Length; i++)
        {
            string[] temp = _movies[i].Split("/");
            filenames[i] = temp[^1]; // file name
        }
        _buttons = new Button[_movies.Length + 1];
        for (int i=0; i < _movies.Length; i++)
        {
            GameObject movieItem = Instantiate(movieItemPrefab, moviesParent.transform);
			TextMeshProUGUI numberText = movieItem.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            numberText.text = (i + 1).ToString();

            TextMeshProUGUI nameText = movieItem.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            nameText.text = "  " + filenames[i];

            Button button = movieItem.transform.GetChild(1).GetComponent<Button>();
			if (i == 0)
			{
				EventSystem.current.SetSelectedGameObject(null);
				EventSystem.current.SetSelectedGameObject(movieItem.transform.GetChild(1).gameObject);
			}
			_buttons[i] = button;

            int index = i;
            button.onClick.AddListener(() => OnMovieSelected(index));
        }

        _buttons[_movies.Length] = backButton;

		for (int i = 0; i < _buttons.Length; i++)
		{
			Navigation navigation = _buttons[i].navigation;
			navigation.mode = Navigation.Mode.Explicit;
			if (i > 0)
			{
				navigation.selectOnLeft = _buttons[i - 1];
				navigation.selectOnUp = _buttons[i - 1];
			}
			if (i < _buttons.Length - 1)
			{
				navigation.selectOnRight = _buttons[i + 1];
				navigation.selectOnDown = _buttons[i + 1];
			}
			_buttons[i].navigation = navigation;
		}
	}
    
    /*
    // Update is called once per frame
    void Update()
    {
        // Return buttons does not exist
        if (_buttons == null || _buttons.Length == 0)
        {
            return;
        }
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
        {
            _selectedButtonIndex = (_selectedButtonIndex - 1 + _buttons.Length) % _buttons.Length;
        }

        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
        {
            _selectedButtonIndex = (_selectedButtonIndex + 1 + _buttons.Length) % _buttons.Length;
        }
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_buttons[_selectedButtonIndex].gameObject);
    }
    */
    private void OnMovieSelected(int i)
    {
		createMenu.OnMovieSelected(i);
        
    }
}
