using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    public AudioSource audioSource;      // The audio source to play clips from
    public AudioClip[] audioClips;       // The array of audio clips
    public float fadeDuration = 1.0f;    // Fade duration in seconds
    public static bool isMalocchioMusicPlaying = false;

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

    private void Start()
    {
        if (audioClips.Length > 0)
        {
            StartCoroutine(PlayNextClip());
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Register scene change event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        if (sceneName == "Malocchio")
        {
            // Fermare la musica corrente
            StopCurrentMusic();
        }
        else if (isMalocchioMusicPlaying && IsProvaScene(sceneName))
        {
            ResumeMusic();
        }
        else
        {
            ResumeMusic();
        }
    }

    public void PlayNewBackgroundMusic(AudioClip newClip)
    {
        isMalocchioMusicPlaying = true;
        audioSource.clip = newClip;
        audioSource.loop = true;
        audioSource.Play();
        DontDestroyOnLoad(audioSource.gameObject);
    }

    private void StopCurrentMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    private void ResumeMusic()
    {
        if (!audioSource.isPlaying && !isMalocchioMusicPlaying)
        {
            audioSource.UnPause();
        }
    }

    private IEnumerator PlayNextClip()
    {
        while (!isMalocchioMusicPlaying)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();

            yield return StartCoroutine(FadeIn(audioSource, fadeDuration));

            yield return new WaitForSeconds(audioSource.clip.length - fadeDuration);

            yield return StartCoroutine(FadeOut(audioSource, fadeDuration));

            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        }
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float startVolume = 0f;
        audioSource.volume = startVolume;

        while (audioSource.volume < 0.03f)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 0.03f;
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

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister scene change event
    }

    private bool IsProvaScene(string sceneName)
    {
        return sceneName == "Prima Prova" || sceneName == "Seconda Prova" ||
               sceneName == "Terza Prova" || sceneName == "Quarta Prova" ||
               sceneName == "Quinta Prova";
    }
}
