using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ������ �� ��������� �������

    private FixedJoint joint; // ��������� FixedJoint ��� ���������� ������ � ��������

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned in CameraController!");
            return;
        }

        // ��������� ��������� FixedJoint � ������� "Camera"
        joint = gameObject.AddComponent<FixedJoint>();
        // ������������� ��������� ������� � �������� connectedBody ��� ����������
        joint.connectedBody = target.GetComponent<Rigidbody>();
    }
}