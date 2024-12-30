using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject winPanel; // Kéo UI Win vào đây
    public GameObject losePanel; // Kéo UI Lose vào đây

    // Hàm gọi khi người chơi thắng
    public void PlayerWin()
    {
        winPanel.SetActive(true); // Hiển thị bảng Win
        losePanel.SetActive(false); // Đảm bảo bảng Lose không hiển thị
        Debug.Log("Player Wins!");
        Time.timeScale = 0f; // Dừng game
    }

    // Hàm gọi khi người chơi thua
    public void PlayerLose()
    {
        losePanel.SetActive(true); // Hiển thị bảng Lose
        winPanel.SetActive(false); // Đảm bảo bảng Win không hiển thị
        Debug.Log("Player Loses!");
        Time.timeScale = 0f; // Dừng game
    }
}
