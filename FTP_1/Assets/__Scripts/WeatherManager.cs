using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    //���� ��������� �������� ����������
    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Weathed manager starting...");
        _network = service; //���������� ������������ ������� NetworkService.
        StartCoroutine(_network.GetWeatherXML(OnXMLDataLoaded));//������ �������� ������ �� ���������
    
        status = ManagerStatus.Initializing;//������ ��������� �� Started �� Initializing.
    }

    /// <summary>
    /// ����� ��������� ������
    /// ����� ����� �������� ������
    /// </summary>
    /// <param name="data"></param>
    public void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);

        status = ManagerStatus.Started;
    }
}
