using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacterMenu : MonoBehaviour
{
    public ChooseCharacterButton buttonPrefab;
    public Transform buttonsParent;

    private List<ChooseCharacterButton> buttonList = new List<ChooseCharacterButton>();
    private List<Item> items;

    void Start()
    {
        items = new List<Item>
        {
            new Item { Id = "1", Img = "image1.jpg" },
        };
        for (int i = 0; i < items.Count; i++)
        {
            ChooseCharacterButton button = Instantiate(buttonPrefab, buttonsParent);
            button.SetCharacterImage(items[i].Img);
            button.chooseCharacterButton.onClick.AddListener(() => { ResetAllButtons(button); });
            buttonList.Add(button);
        }
    }

    public void ResetAllButtons(ChooseCharacterButton button)
    {
        foreach (var chooseCharacterButton in buttonList)
        {
            chooseCharacterButton.ResetState();
        }

        button.ChooseCharacter();
    }
}

class Item
{
    public string Id { get; set; }
    public string Img { get; set; }
}