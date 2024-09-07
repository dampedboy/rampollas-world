using UnityEngine;

public class BookController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject uiPanelTalk;
    [SerializeField] private GameObject player;

    private bool _isOpen = false;
    private bool _isPlayerInside = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    [SerializeField] private PlayerMovement _playerMovement;

    void Start()
    {
        _animator = GetComponent<Animator>();
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
        if (uiPanelTalk != null)
        {
            uiPanelTalk.SetActive(false);
        }

    }

    void Update()
    {
        if (_isPlayerInside)
        {
            if (uiPanelTalk != null)
            {
                uiPanelTalk.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3"))
            {
                if (_isOpen)
                {
                    Close();
                }
                else
                {
                    Open();
                }
            }
        }
        else
        {
            if (uiPanelTalk != null)
            {
                uiPanelTalk.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInside = false;
        }
    }

    public void Open()
    {
        if (_animator == null || _isOpen)
            return;

        _isOpen = true;
        _animator.SetBool("open", true);

        PlaySound(openSound);

        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }

        if (_playerMovement != null)
        {
            _playerMovement.enabled = false;
        }
    }

    public void Close()
    {
        if (_animator == null || !_isOpen)
            return;

        _isOpen = false;
        _animator.SetBool("open", false);

        PlaySound(closeSound);

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

        if (_playerMovement != null)
        {
            _playerMovement.enabled = true;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}

