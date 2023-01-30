using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]//ѕроверка наличи€ компонента
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;//—ценарию нужна ссылка на обьект,
                                               //относительно которого будет происходить перемещение.

    private ControllerColliderHit _contact; //Ќужно дл€ сохранени€ данных о столкновении между функци€ми.




    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float pushForce = 3.0f;

    private CharacterController _charController;
    private float _vertSpeed;
    private Animator _animator;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;//»нициализируем скорость пов ертикали, присваива€ ей мин.
                             //скорость падени€ в начале существующей функции.
        _animator = GetComponent<Animator>();
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

        #region ѕрыжок персонажа и контроль нахождени€ на поверхности
        bool hitGround = false;
        RaycastHit hit;

        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = //–ассто€ние, с которым производитс€ сравнение(слегка выходит за нижнюю часть капсулы
                (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        _animator.SetFloat("Speed",movement.sqrMagnitude);

        if (hitGround)//ѕровер€ет на соприкосновение с землей
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
                //_vertSpeed = -0.1f;
                _animator.SetBool("Jumping", false);
            }
        }
        else //≈сли персонаж не стоит на поверхности, примен€ем гравитацию, пока не будет достигнута скорость.
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }

            if (_contact != null)
            {
                _animator.SetBool("Jumping",true);
            }

            if (_charController.isGrounded)//ћетод бросани€ луча не обнаруживает поверхности
                                           //но капсула с ней соприкасаетс€
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed;
        #endregion

        #region ƒвижение персонажа

        movement *= Time.deltaTime;
        _charController.Move(movement);

        #endregion

    }

    private void OnControllerColliderHit(ControllerColliderHit hit) //ѕри распозновании столкновени€
                                                                    //данные этого столкновени€ сохран€ютс€ в метод обратного вызова.
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;//ѕровер€ее, есть ли у учавствующего в столкновении
        //обьекта компонент Rigidbody, обеспечивающий реакцию на приложенную силу.
        if(body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;//Ќазначение физическому телу скорости.
        }
    }
}
