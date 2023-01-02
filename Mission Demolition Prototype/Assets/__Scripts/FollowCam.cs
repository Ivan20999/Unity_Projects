using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; //Ссылка на интересующий обьект

    [Header("Set Dynamically")]
    public float camZ; //Желаемая координата Z камеры
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        //if(POI == null) return; //Выйти, если нет интересуещего обьекта

        ////получить позицию интересующего обьекта
        //Vector3 destination = POI.transform.position;

        Vector3 destination;
        //Если нет интересуещего обьекта, вернуть P:[0,0,0]
        if(POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            //Получить позицию интересующего обьекта
            destination = POI.transform.position;
            //Если интересующий обьект - снаряд, убедиться, что он остановился
            if(POI.tag == "Projectile")
            {
                //Если он стоит на месте, то есть не двигается
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    //Вернуть исходные настройки поля зрения камеры
                    POI = null;
                    // в селудющем кадре
                    return;
                }
               
            }
        }

        //Ограничить X и Y минимальными значениями
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Определить точку между текущим местоположением камеры destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //Прнудительно установить значение destination.z равным camZ,
        //чтобы отодвинуть камеру подальше
        destination.z = camZ;
        //поместить камеру в позицию destination
        transform.position = destination;
        // Изменить размер orthographicsSize камеры, чтобы земля осталась в поле зрения
        Camera.main.orthographicSize = destination.y + 10;


    }
}
