using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToChooseCharacterMenu()
    {
        SceneManager.LoadSceneAsync("ChooseCharacterMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnOptionButtonClicked(Animator animator)
    {
        animator.SetBool("OptionsMenuState", !animator.GetBool("OptionsMenuState"));
    }
}