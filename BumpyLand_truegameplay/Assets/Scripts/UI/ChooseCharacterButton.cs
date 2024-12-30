using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacterButton : MonoBehaviour
{
    public Button chooseCharacterButton;
    public Button confirmCharacterButton;

    public void ChooseCharacter()
    {
        chooseCharacterButton.gameObject.SetActive(false);
        confirmCharacterButton.gameObject.SetActive(true);
    }

    public void ConfirmCharacter()
    {
        SceneManager.LoadScene("ChooseMapMenu");
    }

    private void Awake()
    {
        confirmCharacterButton.onClick.AddListener(ConfirmCharacter);
        ResetState();
    }

    public void ResetState()
    {
        chooseCharacterButton.gameObject.SetActive(true);
        confirmCharacterButton.gameObject.SetActive(false);
    }
}