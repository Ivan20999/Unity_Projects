using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    //«начение дл€ скорости движени€ и рассто€ни€, с которого
    //начиниаетс€ реакци€ на преп€тствие.
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    private bool _alive;

    [SerializeField] private GameObject _fireballPrefab;
    private GameObject _fireball;

    private void Start()
    {
        _alive = true;
    }

    private void Update()
    {
        //ƒвижение начинаетс€ только в случае живого персонажа.
        if (_alive)
        {
            //Ќепрерывное движение в каждом кадре, несмотр€ на повороты.
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        //Ћуч находитс€ в том же положении и нацеливаетс€ в том же
        // направлении, что и персонаж.
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //Ѕросаем луч с описанной вокруг него окружностью
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            //»грок распознаетс€ тем же способом, что и мишень в сценарии 
            //RayShooter.
            PlayerCharacter target = hitObject.GetComponent<PlayerCharacter>();
            if (target)
            {
                if(_fireball == null)
                {
                    _fireball = Instantiate(_fireballPrefab) as GameObject;
                    //ѕоместим огненный шар перед врагом и нацелим в направлении
                    //его движени€.
                    _fireball.transform.position =
                        transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
            if (hit.distance < obstacleRange)
            {
                //ѕоворот с наполовину случайным выбором направлени€.
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    //ќткрытый метод, позвол€юий внешнему коду воздействовать на 
    //"живое" состо€ние.
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

}
