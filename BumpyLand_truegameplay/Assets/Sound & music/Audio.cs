using UnityEngine;

public class GripSound : MonoBehaviour
{
    public AudioClip touchSound; // Kéo file touch_sfx.MP3 vào đây
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGripSound()
    {
        if (touchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(touchSound);
        }
    }
}
