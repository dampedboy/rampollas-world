using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Portal : MonoBehaviour
{
    private static int portalLevel = 0;
    public TMP_Text portalLevelText;
    public AudioClip updatePortalSoundClip; // AudioClip per il suono di aggiornamento del portale
    public AudioClip loadLevelSoundClip; // AudioClip per il suono di caricamento del livello
    public AudioSource audioSource; // AudioSource per gestire i suoni

    private void Start()
    {
        // Ottieni o aggiungi l'AudioSource a questo GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    private void Update()
    {
        if (portalLevelText != null)
        {
            int pl = portalLevel + 1;
            portalLevelText.text = "Lvl. " + pl;
        }
    }

    public void UpdatePortal()
    {
        if (CoinManager.CoinCount >= 0)
        {
            portalLevel++;
            Debug.Log("Livello attuale del portale: " + portalLevel);

            StartCoroutine(RotatePortalForTime(2f));

            if (portalLevelText != null)
            {
                int pl = portalLevel + 1;
                portalLevelText.text = "Lvl. " + pl;
            }

            // Riproduci il suono di aggiornamento del portale
            if (updatePortalSoundClip != null)
            {
                audioSource.clip = updatePortalSoundClip;
                audioSource.Play();
            }
        }
    }

    private IEnumerator RotatePortalForTime(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.Rotate(Vector3.forward, 90f * Time.deltaTime / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex;
        if (currentSceneIndex == 0)
        {
            nextSceneIndex = currentSceneIndex + 1 + portalLevel;
        }
        else
        {
            nextSceneIndex = currentSceneIndex + 1;
        }

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);

            // Riproduci il suono di caricamento del livello
            if (loadLevelSoundClip != null)
            {
                audioSource.clip = loadLevelSoundClip;
                audioSource.Play();
            }
        }
        else
        {
            Debug.Log("Hai completato tutti i livelli!");
        }
    }
}

