using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Door : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator doorAnim;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            doorAnim.SetTrigger("open");
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            doorAnim.SetTrigger("close");
        }
        
    }
}
