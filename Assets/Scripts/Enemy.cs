using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    [SerializeField] float enemyRange=13f;
    [SerializeField] private float enemyRotationSpeed=5f;
    private Shoot currentShoot;
    private float fireRate;
    private float fireRateDelta;

    void Start()
    {
        currentShoot=GetComponentInChildren<Shoot>();
        fireRate=currentShoot.GetRateOfFIre();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 playerGroundPos= new Vector3(target.position.x,target.position.y,target.position.z);

        if(Vector3.Distance(transform.position,playerGroundPos)>enemyRange)
        {
            return;
        }
        Vector3 playerDirection=playerGroundPos-transform.position;
        float enemyRotationStep=enemyRotationSpeed*Time.deltaTime;
        Vector3 newLookDirection= Vector3.RotateTowards(transform.forward,playerDirection,enemyRotationStep,0f);
        transform.rotation=Quaternion.LookRotation(newLookDirection);
        fireRateDelta-=Time.deltaTime;
        if(fireRateDelta<=0)
        {
            currentShoot.Fire();
            fireRateDelta=fireRate;
        }
        
    }
}
