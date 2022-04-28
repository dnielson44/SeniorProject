using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    
    public float maxSpeed = 3000f;

    public float acceleration = 700f;
    public float breakingForce = 600f;
    public float maxTurnAngle = 50f;
    public float TurnAngle = 10f;
    public float speed = 16f;
    public float turnAcceleration = 100f;
    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;
    public float vel;

    private void Update() {
        Rigidbody rb = GetComponent<Rigidbody>();
        vel = rb.velocity.magnitude;  
        if(vel > 16){
            currentAcceleration = 0f;
            frontLeft.motorTorque = currentAcceleration;
            frontRight.motorTorque = currentAcceleration;
        } 
        
    //}
    //private void FixedUpdate() {

        

        
        if(Input.GetKeyDown("w")){
            if(currentAcceleration < maxSpeed && vel < speed){
                currentAcceleration += acceleration;
            }
        }
        if(Input.GetKeyUp("w")){
            currentAcceleration = 0f;
            //currentAcceleration -= 10f;
        }
        if(Input.GetKeyDown("s")){
            currentAcceleration -= acceleration;
        }
        if(Input.GetKeyUp("s")){
            currentAcceleration = 0f;
        }

        // if(currentAcceleration < maxSpeed){
        //     currentAcceleration = acceleration * Input.GetAxis("Vertical");
        // } else {
        //     currentAcceleration = 300f;
        // }
        


        // apply brakes
        if (Input.GetKey(KeyCode.Space))
            currentBreakForce = breakingForce;
        else
            currentBreakForce = 0f;

        

        // steering
        if(Input.GetKeyDown("a")){
            if(currentTurnAngle < maxTurnAngle){
                if(vel < 8f) {
                    maxTurnAngle = 70f;
                    currentTurnAngle -= 20f;
                } else if (vel > 14f) {
                    maxTurnAngle = 20f;
                    currentTurnAngle -= 5f;
                } else {
                    currentTurnAngle -= TurnAngle;
                }
                
                if(currentAcceleration > 0f && vel > 8f){
                    currentAcceleration -= turnAcceleration;
                }
                
            }
        }
        if(Input.GetKeyUp("a")){
            currentTurnAngle = 0f;
            // currentAcceleration += turnAcceleration;
        }
        if(Input.GetKeyDown("d")){
            if(currentTurnAngle < maxTurnAngle){
                if(vel < 8f) {
                    maxTurnAngle = 70f;
                    currentTurnAngle += 20f;
                } else if (vel > 14f){
                    maxTurnAngle = 20f;
                    currentTurnAngle += 5f;
                } else {
                    currentTurnAngle += TurnAngle;
                }
                
                if(currentAcceleration > 0f && vel > 8f){
                    currentAcceleration -= turnAcceleration;
                }
                
            }
        }
        if(Input.GetKeyUp("d")){
            currentTurnAngle = 0f;
            //currentAcceleration += turnAcceleration;
        }
        
        // if(currentTurnAngle < maxTurnAngle){
        //     currentTurnAngle = TurnAngle * Input.GetAxis("Horizontal");
        // } else {
        //     currentTurnAngle = 0f;
        // }

        // accelerate
        //frontRight.motorTorque = currentAcceleration;
        //frontLeft.motorTorque = currentAcceleration;

        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;
        

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        //steering
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight,frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight,backRightTransform);
    }

    

    void UpdateWheel(WheelCollider col, Transform trans){
        
        // Get wheel collider
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        // set wheel transform
        trans.position = position;
        trans.rotation = rotation;
    }
}
