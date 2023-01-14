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
        //Доступ к другим компонентам, присоединенных к этому же обьекту
        _charController = GetComponent<CharacterController>();

    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        //Ограничение движение по диагонали той же скоростью, что движение
        //параллельно осям.
        movement = Vector3.ClampMagnitude(movement, speed);
        //Использует значение переменной gravity вместо нуля
        movement.y = gravity; 
        movement *= Time.deltaTime;
        //Преобразует вектор движения от локальных к глобальным координатам
        movement = transform.TransformDirection(movement);
        //Заставим этот вектор перемещать компонент CharacterController.
        _charController.Move(movement);
    }
}
