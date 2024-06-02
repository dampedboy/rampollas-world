using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] GameObject uiPanel; // Il pannello della UI


    private bool _open = false;

    void Start()
    {
        _animator = GetComponent<Animator>();


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Open();
        if (Input.GetKeyDown(KeyCode.C))
            Close();
    }

    public void Open()
    {
        if (_animator == null)
            return;

        _open = true;


        _animator.SetBool("open", _open);

        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }

    }

    public void Close()
    {
        if (_animator == null)
            return;

        _open = false;

        _animator.SetBool("open", _open);


        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

    }
}
