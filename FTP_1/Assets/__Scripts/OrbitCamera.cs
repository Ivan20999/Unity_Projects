using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _target; //—ериализованна€ ссылка на обьект, вокруг которого производитс€ облет.

    public float rotSpeed = 1.5f;

    private float _rotY;
    private Vector3 _offset;

    private void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = _target.position - transform.position; //—охранение начального смещени€ между камерой и целью
    }

    private void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0) //ћедленный поворот камеры при помощи клавиш со стрелками...
        {
            _rotY +=horInput * rotSpeed;
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3; //или быстрый поворот с помощью мыши.
        }

        Quaternion rotation = Quaternion.Euler(0,_rotY,0);
        transform.position = _target.position - (rotation * _offset);//ѕоддерживаем начально смещение, сдвигаемое
                                                                     //в соотвествии с поворотом камеры.
        transform.LookAt(_target); // амера всегда направлена на цель, где бы относительной этой она ни располагалась.
    }



}
