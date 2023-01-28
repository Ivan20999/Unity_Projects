using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]//ѕроверка наличи€ компонента
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;//—ценарию нужна ссылка на обьект,
                                               //относительно которого будет происходить перемещение.
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
        _vertSpeed = minFall;//»нициализируем скорость пов ертикали, присваива€ ей мин.
                             //скорость падени€ в начале существующей функции.
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero; //Ќачинаем с вектора (0,0,0), непрерывно добавл€€ компоненты движени€.

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0) //ƒвижени€ обрабатываютс€ только при нажатии клавиш со стрелками
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);//ќграничивает движение по диагонали той же скоростью, что и движение вдоль оси.

            #region ѕоворот персонажа относительно камеры

            Quaternion tmp = _target.rotation; //—охран€ем начальную ориентацию, чтобы вернутьс€ 
                                               //к ней после завершени€ работы с целевым обьектом
            _target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
            movement = _target.TransformDirection(movement); // ѕреобразуем направление движени€ из
                                                             // локальных в глобальные координаты
            _target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
            #endregion



            
        }

        #region ѕрыжок персонажа
        if (_charController.isGrounded)//ѕровер€ет на соприкосновение с землей
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
        else //≈сли персонаж не стоит на поверхности, примен€ем гравитацию, пока не будет достигнута скорость.
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
        }

        movement.y = _vertSpeed;
        #endregion

        #region ƒвижение персонажа

        movement *= Time.deltaTime;
        _charController.Move(movement);
        #endregion

    }
}
