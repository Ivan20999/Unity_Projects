using UnityEngine;
using System.Collections;
using System;

public class NetworkService : MonoBehaviour
{
    private const string xmlApi =
        "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml";

    private bool IsResponseValid(WWW www) //�������� ������ �� ������� ������.
    {
        if(www.error != null)
        {
            Debug.LogWarning("Bad connection");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.LogWarning("Bad data");
            return false;
        }
        else //��� ��
        {
            return true;

        }
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        WWW www = new WWW(url); //HTTP-������, ������������ ����� �������� ���-�������.
        yield return www; //����� � �������� ����������.

        if (!IsResponseValid(www))
        {
            yield break; //���������� ��������� � ������ ������.
        }

        callback(www.text);//������� ����� ���� ������ ��� ��, ��� � �������� �������.
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, callback); //������ �������� ���� yield � ����������� ���� ����� �������
    }

}
