using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseRoomMenu : MonoBehaviour
{
    public ChooseRoomButton buttonPrefab;
    public Transform buttonContainer;
    private List<ChooseRoomButton> buttonList = new List<ChooseRoomButton>();

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            ChooseRoomButton button = Instantiate(buttonPrefab, buttonContainer);
            buttonList.Add(button);
        }
    }

    public void CreateRoom()
    {
        SceneManager.LoadScene("InRoomScene");
    }

    public void QuickJoinRoom()
    {
        SceneManager.LoadScene("InRoomScene");
    }
}