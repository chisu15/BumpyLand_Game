using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    [Header("Game Over Settings")]
    public GameObject losePanel; // Assign the Lose UI Panel here

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player")) // Ensure the player GameObject has the "Player" tag
        {
            Debug.Log("Player hit the lose collider!");
            ShowLoseScreen();
        }
    }

    private void ShowLoseScreen()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true); // Show the Lose UI Panel
            Time.timeScale = 0f; // Pause the game
        }
    }
}
