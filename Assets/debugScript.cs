using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger");
    }

}
