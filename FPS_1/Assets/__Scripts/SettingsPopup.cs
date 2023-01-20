using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true); //Активирему этот обьект, чтобы открыть окно.
    }

    public void Close()
    {
        gameObject.SetActive(false); //Деактивируем обьек, чтобы закрыть окно.
    }
  
    public void OnSubmitName(string name) //Этот метод срабатывает в момент начала ввода данных в текстовое поле.
    {
        PlayerPrefs.SetString("name", name);
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed) //Этот метод срабатывает при изменении ползунка.
    {
        PlayerPrefs.SetFloat("speed", speed);
        Debug.Log("Speed: " + speed);
    }
}
