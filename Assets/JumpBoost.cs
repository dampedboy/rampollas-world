using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    private float originalJumpHeight;
    public float heightMultiplier=3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AumentaSalto(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetSalto(other.gameObject);
        }
    }

    private void AumentaSalto(GameObject player) {
        StarterAssets.ThirdPersonController controller = player.GetComponent<StarterAssets.ThirdPersonController>();
        originalJumpHeight = controller.getJumpHeight();
        controller.setJumpHeight(heightMultiplier* originalJumpHeight);
       
    }

    private void ResetSalto(GameObject player)
    {
        StarterAssets.ThirdPersonController controller = player.GetComponent<StarterAssets.ThirdPersonController>();
        controller.setJumpHeight(originalJumpHeight);
    }

}
