using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScene : MonoBehaviour
{
    public void ReturnToLobby()
    {
        SceneManager.LoadScene("InRoomScene");
    }
}
