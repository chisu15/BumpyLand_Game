using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public void TurnOffAudio()
    {
        PlayerPrefs.SetInt("Music", 0);
    }

    public void TurnOnAudio()
    {
        PlayerPrefs.SetInt("Music", 1);
    }
}
