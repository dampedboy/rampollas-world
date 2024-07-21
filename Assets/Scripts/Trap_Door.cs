using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Door : MonoBehaviour
{
   
    public Animator doorAnim;
    public Transform door;
    public Transform player;

    void Update()
    {
        float distance = Vector3.Distance(player.position,door.position);
        if (distance<=6){
            doorAnim.SetBool("Near",true);
        }
        else{
            doorAnim.SetBool("Near",false);
        }
    }

}
