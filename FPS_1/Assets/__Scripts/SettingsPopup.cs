using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true); //���������� ���� ������, ����� ������� ����.
    }

    public void Close()
    {
        gameObject.SetActive(false); //������������ �����, ����� ������� ����.
    }
  
    public void OnSubmitName(string name) //���� ����� ����������� � ������ ������ ����� ������ � ��������� ����.
    {
        PlayerPrefs.SetString("name", name);
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed) //���� ����� ����������� ��� ��������� ��������.
    {
        PlayerPrefs.SetFloat("speed", speed);
        Debug.Log("Speed: " + speed);
    }
}
