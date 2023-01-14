using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;

    private CharacterController _charController;

    public float gravity = -9.8f;

    private void Start()
    {
        //������ � ������ �����������, �������������� � ����� �� �������
        _charController = GetComponent<CharacterController>();

    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        //����������� �������� �� ��������� ��� �� ���������, ��� ��������
        //����������� ����.
        movement = Vector3.ClampMagnitude(movement, speed);
        //���������� �������� ���������� gravity ������ ����
        movement.y = gravity; 
        movement *= Time.deltaTime;
        //����������� ������ �������� �� ��������� � ���������� �����������
        movement = transform.TransformDirection(movement);
        //�������� ���� ������ ���������� ��������� CharacterController.
        _charController.Move(movement);
    }
}
