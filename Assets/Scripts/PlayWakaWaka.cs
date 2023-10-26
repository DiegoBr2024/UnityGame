using UnityEngine;

public class PlayWakaWaka : MonoBehaviour
{
    public AudioClip Wakaclip1;
    public AudioClip Wakaclip2;

    private AudioSource Audio;

    private static bool switchclip;
    private void OnDestroy()
    {
        Audio = FindObjectOfType<AudioSource>();
        if (Audio != null)
        {
            Audio.PlayOneShot(switchclip ? Wakaclip1 : Wakaclip2);
            switchclip = !switchclip;

        }

    }
}
