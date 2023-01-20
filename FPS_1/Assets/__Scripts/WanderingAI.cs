using UnityEngine;
using System.Collections;


public class WanderingAI : MonoBehaviour
{
    public const float baseSpeed = 3.0f; //Базовая скорость, меняема в соотвествии с положением ползунка.

    //Значение для скорости движения и расстояния, с которого
    //начиниается реакция на препятствие.
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    private bool _alive;

    [SerializeField] private GameObject _fireballPrefab;
    private GameObject _fireball;

    void Awake()
    {
        //Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy()
    {
       // Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    //private void OnSpeedChanged(float value) //Метод, обьявленный в подписчике для события SPEED_CHANGED.
    //{
    //    speed = baseSpeed * value;
    //}

    private void Start()
    {
        _alive = true;
    }
    #region
    private void Update()
    {
        //Движение начинается только в случае живого персонажа.
        if (_alive)
        {
            //Непрерывное движение в каждом кадре, несмотря на повороты.
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        //Луч находится в том же положении и нацеливается в том же
        // направлении, что и персонаж.
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //Бросаем луч с описанной вокруг него окружностью
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            //Игрок распознается тем же способом, что и мишень в сценарии 
            //RayShooter.
            PlayerCharacter target = hitObject.GetComponent<PlayerCharacter>();
            if (target)
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(_fireballPrefab) as GameObject;
                    //Поместим огненный шар перед врагом и нацелим в направлении
                    //его движения.
                    _fireball.transform.position =
                        transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
            if (hit.distance < obstacleRange)
            {
                //Поворот с наполовину случайным выбором направления.
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    //Открытый метод, позволяюий внешнему коду воздействовать на 
    //"живое" состояние.
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
    #endregion


}
