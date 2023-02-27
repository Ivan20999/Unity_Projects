using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    //Сюда добавляем значение облачности
    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Weathed manager starting...");
        _network = service; //Сохранение вставленного обьекта NetworkService.
        StartCoroutine(_network.GetWeatherXML(OnXMLDataLoaded));//Начина загрузку данных из интернета
    
        status = ManagerStatus.Initializing;//Меняем состояние со Started на Initializing.
    }

    /// <summary>
    /// Метод обратного вызова
    /// сразу после загрузки данных
    /// </summary>
    /// <param name="data"></param>
    public void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);

        status = ManagerStatus.Started;
    }
}
