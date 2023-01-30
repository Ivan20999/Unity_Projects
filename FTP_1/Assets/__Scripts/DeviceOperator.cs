using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{

    public float radius = 1.5f;//Расстояние  с которого персонаж может активировать устройство

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Collider[] hitColliders =
                Physics.OverlapSphere(transform.position, radius);//Метод OverlapSphere()
                                                                  //возвращает список ближайших обьектов.

            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > 0.5f) //Сообщение отправляется только
                                                                     //при корректной ориентации персонажа
                {
                    hitCollider.SendMessage("Operate",
                        SendMessageOptions.DontRequireReceiver);// Метод SendMessage() пытается вызвать
                                                                //именованную функцию независимо от типа целевого обьекта.
                 
                }
            }
            
        }
    }

}
