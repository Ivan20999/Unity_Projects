using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    //����, ��������������� � ���������� Unity
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;

    //����, ��������������� �����������
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
        //����� ����� ������ ����, ����� ��������� ��������� ��� ��������
        aimingMode = true;
        //������� ������
        projectile = Instantiate(prefabProjectile) as GameObject;
        //��������� � ����� launchPos;
        projectile.transform.position = launchPos;
        //������� ��� ��������������
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }


}
