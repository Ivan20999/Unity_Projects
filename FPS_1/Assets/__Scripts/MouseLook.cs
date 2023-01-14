using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        //ќбь€вление структуры данных enum, котора€ будет
        //сопоставл€ть имена с параметрами 
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0f;
    public float sensitivityVer = 9.0f; //ќбь€вл€ем переменные, задающее поворот в вертикальной плоскости

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0; //ќбь€вл€ем закрытую переменную дл€ угла поворота по вертикали.

    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if(body!= null)
        {
            body.freezeRotation = true;
        }

    }

    private void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            //¬ращение по горизонтали
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

        }
        else if (axes == RotationAxes.MouseY)
        {
            //¬ращение по вертикали
            //”величиваем угол поворота по вертикали в соотвествии с 
            //перемещени€ми указател€ мыши.
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
            //‘иксируем угол поворота по вертикали в диапазоне, заданном минимальным 
            // и максимальным значением.
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            //—охран€ем одинаковый угол поворота оси Y (т.е.вращение в горизонтальной
            //плоскости отсуствует)
            float rotationY = transform.localEulerAngles.y;
            //—оздаем новый вектор из сохраненных значений поворота.
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

        }
        else
        {
            // омбинированный поворот
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            //ѕриращение угла поворота через значение delta
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            //«начение delta - это величина изменени€ угла поворота.
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX,rotationY,0);
        }

    }


}
