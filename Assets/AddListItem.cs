using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddListItem : MonoBehaviour
{

    public string[] movies = { "kabhi khushi kabhi gum", "kal ho na ho"};

    public GameObject moviesParent;
    public GameObject movieItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        float offset = 60;
        for(int i=0; i < movies.Length; i++)
        {
            GameObject movieItem = Instantiate(movieItemPrefab, moviesParent.transform);
            movieItem.transform.SetLocalPositionAndRotation(new Vector3(movieItemPrefab.transform.position.x,
                movieItemPrefab.transform.position.y - offset, movieItemPrefab.transform.position.z),
                new Quaternion());
            offset += 53;
            TextMeshProUGUI numberText = movieItem.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            numberText.text = (i + 1).ToString();

            TextMeshProUGUI nameText = movieItem.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            nameText.text = "  " + movies[i];

            Button button = movieItem.transform.GetChild(1).GetComponent<Button>();

            //button.onClick.AddListener(Update);
            //button.onClick.AddListener(delegate { TaskWithParameters("Hello"); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
