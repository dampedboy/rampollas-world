using System.Collections;
using UnityEngine;

public class PurpleButtonController : MonoBehaviour
{
    public Animator buttonAnimator;  // Riferimento all'animator del bottone
    public float slowMotionDuration = 5f;  // Durata del rallentamento del tempo
    public float slowMotionScale = 0.5f;   // Fattore di rallentamento del tempo
    public AudioClip buttonPressClip;  // Audio del bottone

    private bool isButtonPressed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press");
            PlayButtonPressSound();
            StartCoroutine(ActivateSlowMotion());
        }
    }

    private void PlayButtonPressSound()
    {
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }

    private IEnumerator ActivateSlowMotion()
    {
        // Rallenta il tempo
        Time.timeScale = slowMotionScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;  // Assicura che la fisica sia sincronizzata con il nuovo timeScale

        // Attende la durata del rallentamento (in tempo reale)
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Ripristina il tempo normale
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
