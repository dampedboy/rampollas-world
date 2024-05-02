using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    [SerializeField] private KeyCode rightInputKey, leftInputKey, forwardInputKey,backInputKey,jumpKey,absorbableKey;
    private float horizontalInput;
    private float verticalInput;

    public float Horizontal{
        get { 
            return horizontalInput; }
    }
    public float Vertical{
        get {
            return verticalInput; 
        }
    }
    public bool Jump{
        get {
            return Input.GetKeyDown(jumpKey);
        }
    }
    public bool Absorbable{
        get{
            return Input.GetKeyDown(absorbableKey);
        }
    }
    private void GetInput(){
        if(Input.GetKey(rightInputKey))
        {
            horizontalInput=1.0f;
        }
        else if(Input.GetKey(leftInputKey))
        {
            horizontalInput=-1.0f;

        }
        else if(Input.GetKey(forwardInputKey))
        {
            verticalInput=1.0f;
        }
        else if(Input.GetKey(backInputKey))
        {
            verticalInput=-1.0f;
        }
        else{
            horizontalInput=0.0f;
            verticalInput=0.0f;
            
        }
    }
    private void Update()
    {
        GetInput();
    }
}
