 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maximumSpeed;
    public float rotationSpeed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float jumpButtonGracePeriod;

    // ySpeed is needed for jump gravity
    private float ySpeed;
    private Animator animator;
    private float originalStepOffset;
    private CharacterController characterController;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput,0,verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)){
        inputMagnitude /= 2;
        }

        
        animator.SetFloat("input Magnitude",inputMagnitude,0.05f,Time.deltaTime);
        float speed = inputMagnitude * maximumSpeed;
        movementDirection.Normalize();
        
        float gravity = Physics.gravity.y * gravityMultiplier;

        if(isJumping && ySpeed >0 && Input.GetButton("Jump") == false)
        {
          gravity *= 2;
        }
        ySpeed += gravity * Time.deltaTime;


        if(characterController.isGrounded){
            lastGroundedTime = Time.time;
        }
        if(Input.GetButtonDown("Jump")){
            jumpButtonPressedTime = Time.time;
        }


        if(Time.time - lastGroundedTime <= jumpButtonGracePeriod){
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("isGrounded",true);
            isGrounded = true;
            animator.SetBool("isJumping",false);
            isJumping = false;
            animator.SetBool("isFalling",false);


        if(Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod){
            ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity);
            animator.SetBool("isJumping",true);
            isJumping = true;
            jumpButtonPressedTime = null;
            lastGroundedTime = null;
        }
        }else{
            characterController.stepOffset = 0;
            animator.SetBool("isGrounded",false);
            isGrounded = false;

            if((isJumping && ySpeed < 0) || ySpeed<-2 ){
                animator.SetBool("isFalling",true);
            } 
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        if(movementDirection!=Vector3.zero){
            
            animator.SetBool("isMoving",true);
            //transform.rotation = movementDirection;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection,Vector3.up);
           
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }
        else{
            animator.SetBool("isMoving",false);
        }

    }
 public void Jump(float jumpForce)
{
    
    
        ySpeed = Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y * gravityMultiplier);
        animator.SetBool("isJumping", true);
        isJumping = true;
        jumpButtonPressedTime = null;
        lastGroundedTime = null;
    
}
}