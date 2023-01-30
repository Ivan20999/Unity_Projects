using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; } //Свойство читается откуда угоджно,
                                                      //но задается только в этом сценарии.
    
    public void Startup()
    {
        Debug.Log("Invertory manager starting...");//Сюда идут все задачи запуска с долгим
        //временем выполнения.
        status = ManagerStatus.Started;
    }
}
