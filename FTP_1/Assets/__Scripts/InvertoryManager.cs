using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; } //�������� �������� ������ �������,
                                                      //�� �������� ������ � ���� ��������.
    
    public void Startup()
    {
        Debug.Log("Invertory manager starting...");//���� ���� ��� ������ ������� � ������
        //�������� ����������.
        status = ManagerStatus.Started;
    }
}
