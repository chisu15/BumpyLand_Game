using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacterMenu : MonoBehaviour
{
    public ChooseCharacterButton buttonPrefab;
    public Transform buttonsParent;

    private List<ChooseCharacterButton> buttonList = new List<ChooseCharacterButton>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            ChooseCharacterButton button = Instantiate(buttonPrefab, buttonsParent);
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