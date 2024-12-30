using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool IsPaused = false;

    public GameObject optionMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
        optionMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        IsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnOptionButtonClicked()
    {
        optionMenu.SetActive(!optionMenu.activeInHierarchy);
    }
}