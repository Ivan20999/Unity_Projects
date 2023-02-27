using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Проверка на существование различных диспетчеров
/// </summary>
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InvertoryManager))]
[RequireComponent(typeof(WeatherManager))]

public class Manager : MonoBehaviour
{
    /// <summary>
    /// Статические свойства, которыми остальной код пользуется для
    /// доступа к диспетчерам.
    /// </summary>
    public static PlayerManager Player { get; private set; }
    public static InvertoryManager Inventory { get; private set; }
    public static WeatherManager Weather { get; private set; }


    /// <summary>
    /// Список диспетчеров, который просматривается в цикле
    /// во время стартовой  последовательности.
    /// </summary>
    private List<IGameManager> _startSequence;

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InvertoryManager>();
        Weather = GetComponent<WeatherManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);
        _startSequence.Add(Weather);

        StartCoroutine(StartupManagers());//Асинхронно загружаем стартовую последовательность
    }

    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();//Создание экземпляра обьекта NetwokService
                                                      //для вставик во все диспетчеры.

        foreach(IGameManager manager in _startSequence)
        {
            manager.Startup(network);
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) //Продолжаем цикл, пока не начнут работать вси диспетчеры.
        {
            int lastReady = numReady;
            numReady = 0;
         
            foreach (IGameManager manager in _startSequence)
            {
                if(manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            
            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
                yield return null;//Остановка на один кадр перед следующей проверкой.
            }
            Debug.Log("All managers started up");
        }
    }

}
