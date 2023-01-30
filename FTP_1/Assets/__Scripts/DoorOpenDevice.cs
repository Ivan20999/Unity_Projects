using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 _dPos;//Смещение, применяемое при открывании двери.

    private bool _open;//Переменная типа Boolean для слежения за открытым состоянием двери.

    public void Operate()
    {
        if (_open) //Открываем или закрываем дверь в зависимости от ее состояния.
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

