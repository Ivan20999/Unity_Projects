using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� �� ������������� ��������� �����������
/// </summary>
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InvertoryManager))]

public class Manager : MonoBehaviour
{
    /// <summary>
    /// ����������� ��������, �������� ��������� ��� ���������� ���
    /// ������� � �����������.
    /// </summary>
    public static PlayerManager Player { get; private set; }
    public static InvertoryManager Inventory { get; private set; }


    /// <summary>
    /// ������ �����������, ������� ��������������� � �����
    /// �� ����� ���������  ������������������.
    /// </summary>
    private List<IGameManager> _startSequence;

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InvertoryManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);

        StartCoroutine(StartupManagers());//���������� ��������� ��������� ������������������
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
