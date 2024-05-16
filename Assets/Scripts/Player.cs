using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private float currentMoveSpeedHorizontal;
    private float currentMoveSpeedVertical;
    [SerializeField] private float moveSpeed=10f;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float gravity=50f;
    [SerializeField] private float terminalSpeed=-55f;
    [SerializeField] private bool canMove=true;
    [SerializeField] private float jumpPower=5f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundedDistance=1.0f;
    [SerializeField] private Transform target_1;
    [SerializeField] private Transform target_2;
    public float speed;
    [SerializeField] private float playerRange=10f;
    private Player_Input input;
    
    private void Awake()
    {
        input=GetComponent<Player_Input>();
    }
    private void CheckGrounded()
    {
        RaycastHit hitInfo;
        isGrounded=Physics.Raycast(transform.position,Vector3.down,out hitInfo,groundedDistance);
        if(isGrounded)
        {
            verticalSpeed=0f;
            float yPoint=hitInfo.point.y+groundedDistance;
            transform.position=new Vector3(transform.position.x,yPoint,transform.position.z);
        }
    }
    private void Fall()
    {
        if(isGrounded)
            return;
        if(verticalSpeed>terminalSpeed)
        {
             verticalSpeed = verticalSpeed-(gravity*Time.deltaTime);
        }
        if(verticalSpeed<=terminalSpeed)
        {
            verticalSpeed=terminalSpeed;
        }
        transform.position+=new Vector3(0f,verticalSpeed*Time.deltaTime,0f);
    }
    private void DebugDrawRays()
    {
        Debug.DrawRay(transform.position,-new Vector3(0f,groundedDistance,0f),Color.green);
    }
    private void Move()
        {
            if(!canMove)
                return;
            currentMoveSpeedHorizontal=moveSpeed*input.Horizontal;
            currentMoveSpeedVertical=moveSpeed*input.Vertical;
            transform.position+=new Vector3(currentMoveSpeedHorizontal*Time.deltaTime, 0,currentMoveSpeedVertical*Time.deltaTime);

        }
    private void Jump()
    {
        if(isGrounded&&input.Jump)
        {
            verticalSpeed=jumpPower;
            isGrounded=false;
        }
    }
    private void Absorbe()
    {
        if(input.Absorbable){
             target_1.transform.position=Vector3.MoveTowards(target_1.transform.position,target_2.transform.position,speed);
        }
       
    }
    private void Collect()
    {
        
        Vector3 objectGroundPos= new Vector3(target_1.position.x,target_1.position.y,target_1.position.z);
        if(Vector3.Distance(transform.position,objectGroundPos)>playerRange){
            return;
        }
        else{
            Absorbe();
        }
        
       
       
    }

    
    // Update is called once per frame
    void Update()
    {
       DebugDrawRays();
       CheckGrounded();
       Jump();
       Fall(); 
       Move();
       Collect(); 
    }
}
