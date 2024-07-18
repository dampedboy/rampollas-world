using System.Collections;
using UnityEngine;

public class GreenButtonController : MonoBehaviour
{
    public Animator buttonAnimator;
    public GameObject[] platforms;
    public float platformsVisibleDuration = 5f;
    public AudioClip buttonPressClip;
    public AudioClip cageSoundClip;

    private bool isButtonPressed = false;

    void Start()
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press");
            PlayButtonPressSound();
            StartCoroutine(ShowPlatformsTemporarily());
        }
    }

    private void PlayButtonPressSound()
    {
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position); 
    }

    private void PlayCageSound()
    {
        AudioSource.PlayClipAtPoint(cageSoundClip, transform.position, 3f); // Increase volume by 1.5 times
    }

    private IEnumerator ShowPlatformsTemporarily()
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }

        yield return new WaitForSeconds(platformsVisibleDuration);

        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }

        PlayCageSound();
    }
}
