using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMapMenu : MonoBehaviour
{
    public ChooseMapButton buttonPrefab;
    public Transform buttonsParent;
    private List<ChooseMapButton> buttonList = new List<ChooseMapButton>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            ChooseMapButton button = Instantiate(buttonPrefab, buttonsParent);
            buttonList.Add(button);
        }
    }


    public void GoToChooseCharacterMenu()
    {
        SceneManager.LoadScene("ChooseCharacterMenu");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("InRoomScene");
    }
}
