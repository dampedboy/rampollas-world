using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire=1f;
    public float GetRateOfFIre()
    {
        return rateOfFire;
    }

    public void Fire()
    {
        Instantiate(projectile,transform.position,transform.rotation);
    }
    
}
