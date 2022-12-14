using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; //—сылка на интересующий обьект

    [Header("Set Dynamically")]
    public float camZ; //∆елаема€ координата Z камеры
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        if(POI == null) return; //¬ыйти, если нет интересуещего обьекта

        //получить позицию интересующего обьекта
        Vector3 destination = POI.transform.position;
        //ќграничить X и Y минимальными значени€ми
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //ќпределить точку между текущим местоположением камеры destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //ѕрнудительно установить значение destination.z равным camZ,
        //чтобы отодвинуть камеру подальше
        destination.z = camZ;
        //поместить камеру в позицию destination
        transform.position = destination;
        // »зменить размер orthographicsSize камеры, чтобы земл€ осталась в поле зрени€
        Camera.main.orthographicSize = destination.y + 10;


    }
}
