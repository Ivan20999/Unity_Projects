using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� �� ������������� ��������� �����������
/// </summary>
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InvertoryManager))]
[RequireComponent(typeof(WeatherManager))]

public class Manager : MonoBehaviour
{
    /// <summary>
    /// ����������� ��������, �������� ��������� ��� ���������� ���
    /// ������� � �����������.
    /// </summary>
    public static PlayerManager Player { get; private set; }
    public static InvertoryManager Inventory { get; private set; }
    public static WeatherManager Weather { get; private set; }


    /// <summary>
    /// ������ �����������, ������� ��������������� � �����
    /// �� ����� ���������  ������������������.
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

        StartCoroutine(StartupManagers());//���������� ��������� ��������� ������������������
    }

    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();//�������� ���������� ������� NetwokService
                                                      //��� ������� �� ��� ����������.

        foreach(IGameManager manager in _startSequence)
        {
            manager.Startup(network);
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) //���������� ����, ���� �� ������ �������� ��� ����������.
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
                yield return null;//��������� �� ���� ���� ����� ��������� ���������.
            }
            Debug.Log("All managers started up");
        }
    }

}
