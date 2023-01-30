using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ѕроверка на существование различных диспетчеров
/// </summary>
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InvertoryManager))]

public class Manager : MonoBehaviour
{
    /// <summary>
    /// —татические свойства, которыми остальной код пользуетс€ дл€
    /// доступа к диспетчерам.
    /// </summary>
    public static PlayerManager Player { get; private set; }
    public static InvertoryManager Inventory { get; private set; }


    /// <summary>
    /// —писок диспетчеров, который просматриваетс€ в цикле
    /// во врем€ стартовой  последовательности.
    /// </summary>
    private List<IGameManager> _startSequence;

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InvertoryManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);

        StartCoroutine(StartupManagers());//јсинхронно загружаем стартовую последовательность
    }

    private IEnumerator StartupManagers()
    {
        foreach(IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) //ѕродолжаем цикл, пока не начнут работать вси диспетчеры.
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
                yield return null;//ќстановка на один кадр перед следующей проверкой.
            }
            Debug.Log("All managers started up");
        }
    }

}
