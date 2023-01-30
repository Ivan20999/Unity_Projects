using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string _itemName; //Имя элемента

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Item collected: " + _itemName);
        Destroy(this.gameObject);
    }
}
