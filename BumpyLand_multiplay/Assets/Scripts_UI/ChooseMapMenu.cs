using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMapMenu : MonoBehaviour
{
    public ChooseMapButton buttonPrefab;
    public Transform buttonsParent;
    private List<ChooseMapButton> buttonList = new List<ChooseMapButton>();

    private List<MapItem> items;


    void Start()
    {
        items = new List<MapItem>
        {
            new MapItem { Id = "1", Img = "image1.jpg" },
        };
        ;
        for (int i = 0; i < items.Count; i++)
        {
            ChooseMapButton button = Instantiate(buttonPrefab, buttonsParent);
            button.SetMapImage(items[i].Img);
            buttonList.Add(button);
        }
    }


    public void GoToChooseCharacterMenu()
    {
        SceneManager.LoadScene("ChooseCharacterMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Demo");
    }
}

class MapItem
{
    public string Id { get; set; }
    public string Img { get; set; }
}