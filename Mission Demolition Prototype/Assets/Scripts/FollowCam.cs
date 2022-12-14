using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; //������ �� ������������ ������

    [Header("Set Dynamically")]
    public float camZ; //�������� ���������� Z ������
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        if(POI == null) return; //�����, ���� ��� ������������� �������

        //�������� ������� ������������� �������
        Vector3 destination = POI.transform.position;
        //���������� X � Y ������������ ����������
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //���������� ����� ����� ������� ��������������� ������ destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //������������ ���������� �������� destination.z ������ camZ,
        //����� ���������� ������ ��������
        destination.z = camZ;
        //��������� ������ � ������� destination
        transform.position = destination;
        // �������� ������ orthographicsSize ������, ����� ����� �������� � ���� ������
        Camera.main.orthographicSize = destination.y + 10;


    }
}