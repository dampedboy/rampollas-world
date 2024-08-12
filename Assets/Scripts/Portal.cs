using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Linq;

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
    
    public Animator transition;
    public float transitionTime = 1f;

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
        if (CoinManager.CoinCount >= 8)
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

        if (currentSceneIndex == 30)
        {
            SceneManager.LoadScene(1);
        }
        if (currentSceneIndex == 1)
        {
            nextSceneIndex = currentSceneIndex + 1 + portalLevel;
        }
        else
        {
            nextSceneIndex = currentSceneIndex + 1;
        }

        if (nextSceneIndex < 31)
        {
            SceneManager.LoadScene(nextSceneIndex);

            // Riproduci il suono di caricamento del livello
            if (loadLevelSoundClip != null)
            {
                audioSource.clip = loadLevelSoundClip;
                audioSource.Play();
            }
            // Carica il prossimo livello
            //SceneManager.LoadScene(nextSceneIndex);
            StartCoroutine(LoadLevel(nextSceneIndex));
        }
        else
        {
            Debug.Log("Hai completato tutti i livelli!");
        }
    }

    IEnumerator LoadLevel(int nextSceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(nextSceneIndex);
    }
}

