using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatusHandler : MonoBehaviour
{
    [SerializeField] private GameObject deathUIPanel; 
    [SerializeField] private GameObject winUIPanel;  
    private bool isGameOver = false;

    private void Start()
    {
        if (deathUIPanel != null) deathUIPanel.SetActive(false);
        if (winUIPanel != null) winUIPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameOver) return;

        if (collision.gameObject.CompareTag("DeathObject"))
        {
            HandleGameOver(isWin: false);
        }
        else if (collision.gameObject.CompareTag("WinObject"))
        {
            HandleGameOver(isWin: true);
        }
    }

    private void HandleGameOver(bool isWin)
    {
        isGameOver = true;

        Time.timeScale = 0f;

        if (isWin)
        {
            if (winUIPanel != null) winUIPanel.SetActive(true);
        }
        else
        {
            if (deathUIPanel != null) deathUIPanel.SetActive(true);
        }
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
