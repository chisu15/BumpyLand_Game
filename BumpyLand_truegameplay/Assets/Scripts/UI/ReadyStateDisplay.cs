using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyState : MonoBehaviour
{
    public Image readyStateImage;
    public Image notReadyStateImage;

    public void OnReadyButtonClicked()
    {
        notReadyStateImage.gameObject.SetActive(false);
        readyStateImage.gameObject.SetActive(true);
    }

    public void OnCancelButtonClicked()
    {
        notReadyStateImage.gameObject.SetActive(true);
        readyStateImage.gameObject.SetActive(false);
    }
}