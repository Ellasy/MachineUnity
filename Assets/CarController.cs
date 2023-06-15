using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float motorForce = 1000f;       // Сила двигателя машинки
    public float brakeForce = 2000f;       // Сила тормоза машинки

    private Rigidbody carRigidbody;
    private HingeJoint[] wheelJoints;
    private JointMotor[] wheelMotors;

    private bool isMotorOn = false;        // Флаг, указывающий, включен ли двигатель машинки
    private float steeringAngle = 0f;      // Угол поворота машинки
    private float maxSteeringAngle = 30f;  // Максимальный угол поворота машинки

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();

        // Получаем все HingeJoint компоненты на колесах
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
        // Обработка ввода для управления машинкой
        float motorInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");
        bool brakeInput = Input.GetKey(KeyCode.Space);

        // Включение/выключение двигателя машинки
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMotorOn = !isMotorOn;
        }

        // Установка угла поворота машинки
        steeringAngle = steeringInput * maxSteeringAngle;

        // Применение силы двигателя и тормоза к колесам машинки
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

            // Установка угла поворота колес
            JointSpring jointSpring = wheelJoints[i].spring;
            jointSpring.targetPosition = steeringAngle;
            wheelJoints[i].spring = jointSpring;

            // Применение тормоза
            WheelCollider wheelCollider = wheelJoints[i].GetComponent<WheelCollider>();
            wheelCollider.brakeTorque = brakeInput ? brakeForce : 0f;
        }
    }
}