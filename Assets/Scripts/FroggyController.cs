using UnityEngine;

public class FroggyController : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    public AudioClip soundClip; // Assign your sound clip in the Inspector

    private bool wake = false;
    private bool isPlayerInside = false;
    private bool soundPlaying = false; // Track if the sound is currently playing
    private bool animationActivated = false; // Track if animation has been activated

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not already present
        }
        _audioSource.volume = 0.3f; // Set the volume to 0.5
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.C))
            Wake();
        if (!isPlayerInside)
            Sleep();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

            if (!animationActivated && !soundPlaying) // If animation not activated and sound not playing
            {
                PlaySound();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    public void Wake()
    {
        if (_animator == null)
            return;

        wake = true;
        _animator.SetBool("wake_up", wake);
        animationActivated = true; // Animation is now activated

        if (soundPlaying)
        {
            _audioSource.Stop();
            soundPlaying = false;
        }
    }

    public void Sleep()
    {
        if (_animator == null)
            return;

        wake = false;
        _animator.SetBool("wake_up", wake);

        if (soundPlaying)
        {
            _audioSource.Stop();
            soundPlaying = false;
        }
    }

    private void PlaySound()
    {
        if (_audioSource != null && soundClip != null)
        {
            _audioSource.clip = soundClip;
            _audioSource.Play();
            soundPlaying = true;
        }
    }
}
