using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMapButton : MonoBehaviour
{
    public Button chosenMap;
    public Button unchosenMap;
    public Image mapImage;

    public void OnChosenMapClicked()
    {
        chosenMap.gameObject.SetActive(false);
        unchosenMap.gameObject.SetActive(true);
    }

    public void OnUnchosenMapClicked()
    {
        chosenMap.gameObject.SetActive(true);
        unchosenMap.gameObject.SetActive(false);
    }

    public void SetMapImage(String path)
    {
        mapImage.sprite = Resources.Load<Sprite>(path);
    }
}