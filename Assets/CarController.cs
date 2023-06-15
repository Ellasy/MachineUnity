using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float motorForce = 1000f;      

    public Transform[] wheels;            

    private Rigidbody carRigidbody;

    private bool isMotorOn = false;        
    private float steeringAngle = 0f;      
    private float maxSteeringAngle = 30f;  

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
 
        float motorInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.M))
        {
            isMotorOn = !isMotorOn;
        }

        steeringAngle = steeringInput * maxSteeringAngle;

        for (int i = 0; i < wheels.Length; i++)
        {

            wheels[i].localRotation = Quaternion.Euler(0f, steeringAngle, 0f);

            if (isMotorOn)
            {
                wheels[i].Translate(Vector3.forward * -motorInput * motorForce * Time.deltaTime);
            }

        }
    }
}