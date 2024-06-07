using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed=5f;
    [SerializeField] private int timeToLive = 10;


    private void Update()
    {

        while (timeToLive > 0)
        {
            timeToLive -= Convert.ToInt32(Time.deltaTime);
            Debug.Log(timeToLive + " Fuck you unity");
            if (timeToLive == 0)
            {
                Debug.Log("Kill mee");
            }
            
        }
        transform.Translate(new Vector3(0f, 0f, projectileSpeed * Time.deltaTime));

    }
    
}
