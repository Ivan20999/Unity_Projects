using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 _dPos;//��������, ����������� ��� ���������� �����.

    private bool _open;//���������� ���� Boolean ��� �������� �� �������� ���������� �����.

    public void Operate()
    {
        if (_open) //��������� ��� ��������� ����� � ����������� �� �� ���������.
        {
            Vector3 pos = transform.position - _dPos;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position + _dPos;
            transform.position = pos;
        }
        _open = !_open;
        print("open" + _open);
    }

    public void Activate()
    {
        if (!_open)
        {
            Vector3 pos = transform.position + _dPos;
            transform.position = pos;
            _open = true;
        }
    }

    public void Deactivate()
    {
        if (_open)
        {
            Vector3 pos = transform.position - _dPos;
            transform.position = pos;
            _open = false;
        }
    }
}

