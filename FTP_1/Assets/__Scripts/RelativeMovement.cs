using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]//�������� ������� ����������
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;//�������� ����� ������ �� ������,
                                               //������������ �������� ����� ����������� �����������.
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private CharacterController _charController;
    private float _vertSpeed;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;//�������������� �������� ��� ��������, ���������� �� ���.
                             //�������� ������� � ������ ������������ �������.
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero; //�������� � ������� (0,0,0), ���������� �������� ���������� ��������.

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0) //�������� �������������� ������ ��� ������� ������ �� ���������
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);//������������ �������� �� ��������� ��� �� ���������, ��� � �������� ����� ���.

            #region ������� ��������� ������������ ������

            Quaternion tmp = _target.rotation; //��������� ��������� ����������, ����� ��������� 
                                               //� ��� ����� ���������� ������ � ������� ��������
            _target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
            movement = _target.TransformDirection(movement); // ����������� ����������� �������� ��
                                                             // ��������� � ���������� ����������
            _target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
            #endregion



            
        }

        #region ������ ���������
        if (_charController.isGrounded)//��������� �� ��������������� � ������
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
            }
        }
        else //���� �������� �� ����� �� �����������, ��������� ����������, ���� �� ����� ���������� ��������.
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
        }

        movement.y = _vertSpeed;
        #endregion

        #region �������� ���������

        movement *= Time.deltaTime;
        _charController.Move(movement);
        #endregion

    }
}