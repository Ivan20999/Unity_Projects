using UnityEngine;
using System.Collections;
using System;

public class NetworkService : MonoBehaviour
{
    private const string xmlApi =
        "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml";

    private bool IsResponseValid(WWW www) //Проверка ответа на наличие ошибок.
    {
        if(www.error != null)
        {
            Debug.LogWarning("Bad connection");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.LogWarning("Bad data");
            return false;
        }
        else //Все ок
        {
            return true;

        }
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        WWW www = new WWW(url); //HTTP-запрос, отправленный путем создания веб-обьекта.
        yield return www; //Пауза в процессе скачивания.

        if (!IsResponseValid(www))
        {
            yield break; //Прерывание программы в случае ошибки.
        }

        callback(www.text);//Делегат может быть вызван так же, как и исходная функция.
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, callback); //Каскад ключевых слов yield в вызвывающих друг друга методах
    }

}
