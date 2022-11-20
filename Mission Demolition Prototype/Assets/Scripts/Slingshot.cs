using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    //Поля, устанавливаемые в инспекторе Unity
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;

    //Поля, устанавливаемые динамически
    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    private void OnMouseEnter()
    {
       // print("Slingshot:OnMouseEnter()");
       launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        // print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        //Игрок нажал кнопку мыши, когда указатель находится над рогаткой
        aimingMode = true;
        //Создать снаряд
        projectile = Instantiate(prefabProjectile) as GameObject;
        //Поместить в точку launchPos;
        projectile.transform.position = launchPos;
        //Сделать его кинематическим
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }


}
