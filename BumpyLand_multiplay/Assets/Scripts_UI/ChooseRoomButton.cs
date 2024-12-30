using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseRoomButton : MonoBehaviour
{
    public Button chooseRoomButton;
    
    private void Awake()
    {
        chooseRoomButton.onClick.AddListener(ChooseRoom);
    }
    public void ChooseRoom()
    {
        SceneManager.LoadScene("InRoomMenu");
    }
}
