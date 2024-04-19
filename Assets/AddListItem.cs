using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class AddListItem : MonoBehaviour
{
    public string folderPath = "Assets/Movies"; // Path to the folder containing videos
    public GameObject moviesParent;
    public GameObject movieItemPrefab;
    public CreateMenu createMenu;
    public Button backButton;
    
    private string[] _movies;
    private Button[] _buttons;
    private int _selectedButtonIndex;
    // Start is called before the first frame update
    void Start()
    {
        _movies = Directory.GetFiles(folderPath, "*.mp4");
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
            _buttons[i] = button;

            int index = i;
            button.onClick.AddListener(() => OnMovieSelected(index));
        }

        _buttons[_movies.Length] = backButton;
    }
    

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

    private void OnMovieSelected(int i)
    {
        createMenu.OnMovieSelected(_movies[i]);
    }
}
