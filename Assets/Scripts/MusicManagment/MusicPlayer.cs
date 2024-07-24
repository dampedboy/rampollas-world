using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    
    public AudioSource audioSource;      // La sorgente audio da cui verranno riprodotti i clip
    public AudioClip[] audioClips;       // L'array di clip audio
    public float fadeDuration = 1.0f;    // Durata della dissolvenza in secondi

    private int currentClipIndex = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioClips.Length > 0)
        {
            StartCoroutine(PlayNextClip());
        }
    }

    private IEnumerator PlayNextClip()
    {
        while (true)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            
            Debug.Log("sto suonando la traccia " + currentClipIndex);
            
            yield return StartCoroutine(FadeIn(audioSource, fadeDuration));

            yield return new WaitForSeconds(audioSource.clip.length - fadeDuration);

            yield return StartCoroutine(FadeOut(audioSource, fadeDuration));

            currentClipIndex = (currentClipIndex + 1);
            if (currentClipIndex == audioClips.Length)
            {
                currentClipIndex = 0;
            }
        }
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float startVolume = 0f;
        audioSource.volume = startVolume;

        while (audioSource.volume < 0.05f)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 0.05f;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 0f;
    }
}

