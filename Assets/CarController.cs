using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float motorForce = 1000f;       // ���� ��������� �������
    public float brakeForce = 2000f;       // ���� ������� �������

    private Rigidbody carRigidbody;
    private HingeJoint[] wheelJoints;
    private JointMotor[] wheelMotors;

    private bool isMotorOn = false;        // ����, �����������, ������� �� ��������� �������
    private float steeringAngle = 0f;      // ���� �������� �������
    private float maxSteeringAngle = 30f;  // ������������ ���� �������� �������

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();

        // �������� ��� HingeJoint ���������� �� �������
        WheelCollider[] wheelColliders = GetComponentsInChildren<WheelCollider>();
        wheelJoints = new HingeJoint[wheelColliders.Length];
        wheelMotors = new JointMotor[wheelColliders.Length];

        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelJoints[i] = wheelColliders[i].GetComponent<HingeJoint>();
            wheelMotors[i] = wheelJoints[i].motor;
        }
    }

    private void Update()
    {
        // ��������� ����� ��� ���������� ��������
        float motorInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");
        bool brakeInput = Input.GetKey(KeyCode.Space);

        // ���������/���������� ��������� �������
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMotorOn = !isMotorOn;
        }

        // ��������� ���� �������� �������
        steeringAngle = steeringInput * maxSteeringAngle;

        // ���������� ���� ��������� � ������� � ������� �������
        for (int i = 0; i < wheelJoints.Length; i++)
        {
            JointMotor motor = wheelMotors[i];

            if (isMotorOn)
            {
                motor.force = motorForce;
                motor.targetVelocity = -motorInput * motorForce;
            }
            else
            {
                motor.force = 0f;
                motor.targetVelocity = 0f;
            }

            wheelMotors[i] = motor;

            wheelJoints[i].motor = wheelMotors[i];

            // ��������� ���� �������� �����
            JointSpring jointSpring = wheelJoints[i].spring;
            jointSpring.targetPosition = steeringAngle;
            wheelJoints[i].spring = jointSpring;

            // ���������� �������
            WheelCollider wheelCollider = wheelJoints[i].GetComponent<WheelCollider>();
            wheelCollider.brakeTorque = brakeInput ? brakeForce : 0f;
        }
    }
}