using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ссылка на трансформ машинки

    private FixedJoint joint; // Компонент FixedJoint для соединения камеры с машинкой

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned in CameraController!");
            return;
        }

        // Добавляем компонент FixedJoint к объекту "Camera"
        joint = gameObject.AddComponent<FixedJoint>();
        // Устанавливаем трансформ машинки в качестве connectedBody для соединения
        joint.connectedBody = target.GetComponent<Rigidbody>();
    }
}