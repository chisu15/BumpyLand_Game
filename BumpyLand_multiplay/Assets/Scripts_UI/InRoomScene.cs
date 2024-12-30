using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InRoomScene : MonoBehaviour
{
    public Button readyButton;
    public Button cancelButton;
    public ReadyState statePrefab;

    public void OnReadyButtonClicked()
    {
        readyButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(true);
        statePrefab.OnReadyButtonClicked();
    }
    
    public void OnCancelButtonClicked()
    {
        readyButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(false);
        statePrefab.OnCancelButtonClicked();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Demo");
    }
}
