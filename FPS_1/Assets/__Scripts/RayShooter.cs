using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //����������� ���������� ��� UI-�������. 

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        //������ � ������ �����������, �������������� � ����� �� �������
        _camera = GetComponent<Camera>();
        //�������� ��������� ���� ������� ������.
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        //������� GUI Label() ���������� �� ������ ������.
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        //������� �� ������� ������ ���� + ���������, ��� GUI �� ������������
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //�������� ������ - ��� �������� ��� ������ � ������
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            //�������� � ���� ����� ���� ������� ScreenPointToRay().
            Ray ray = _camera.ScreenPointToRay(point);
            //���������� ��� ����������� �����������
            RaycastHit hit;
            //����������, �� ������� ��������� ���
            if (Physics.Raycast(ray, out hit))
            {
                //�������� ������ � ������� ����� ���.
                GameObject hitObject = hit.transform.parent.gameObject;

                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                //��������� ������� � ����� ������� ���������� ReactiveTarget.
                if (target != null)
                {
                    //Debug.Log("Target hit");
                    //����� ������ ��� ������ ������ ��������� ����������� ���������.
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT); //� ������� �� ��������� ����������� �������� ���������.
                }
                else
                {
                    //������ ����������� � ����� �� ���������.
                    StartCoroutine(SphereIndicator(hit.point));
                }
                //��������� ���������� �����, � ������� ����� ���
                //Debug.Log("Hit " + hit.point);
            }

        }

    }

    //����������� ���������� ��������� IEnummerator
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        //�������� ����� yieald ��������� �����������, ����� ������� ������������.
        yield return new WaitForSeconds(1);

        //������� ���� GameObject � ������� ������
        Destroy(sphere);
    }
}
