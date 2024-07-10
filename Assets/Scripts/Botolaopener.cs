using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Botolaopener : MonoBehaviour
{
    private Animator _animator;
    private bool _open = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontriggerenter");
        Open();
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("ontriggerexit");
        Close();
    }

    public void Open()
    {
        if (_animator == null)
        {
            Debug.Log("animator null");
            return;
        }
        
        Debug.Log("animator open");
        _animator.SetBool("open", true);
    }

    public void Close()
    {
        if (_animator == null)
        {
            Debug.Log("animator null");
            return;
        }
        
        Debug.Log("animator close");
        _animator.SetBool("open", false);
    }
    
}
