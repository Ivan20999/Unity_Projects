using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _targets;//Список целевых обьектов, которые будут
                                                   //активировать данный триггер.
    public bool requireKey;

    private void OnTriggerEnter(Collider other)
    {
        if (requireKey && Manager.Inventory.equippedItem != "key")
        {
            return;
        }

        foreach (GameObject target in _targets)
        {
            target.SendMessage("Activate");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in _targets)
        {
            target.SendMessage("Deactivate");
        }
    }

}
