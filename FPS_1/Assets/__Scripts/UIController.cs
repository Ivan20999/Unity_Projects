using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; //Импорт инфроструктуры для работы с кодом UI.

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scroleLabel; //Обьект сцены Reference Text, предназначенный
                                                //для задания свойства text.
    private void Update()
    {
        _scroleLabel.text = Time.realtimeSinceStartup.ToString();
    }

    public void OnOpenSettings() //Метод, вызывающий кнопки настроек
    {
        Debug.Log("open settings");
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down");
    }
}
