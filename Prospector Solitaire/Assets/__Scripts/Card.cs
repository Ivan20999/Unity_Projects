using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //Будет определен позже

 
    
}

[System.Serializable] //Сериализуемый класс доступен для правки в инспекторе
public class Decorator
{
    //Этот класс хранит информацию из DeckXML о каждом значке на карте
    public string type; //Значок, определяющий достоинство карты, имеет
                        //type = "pip"
    public Vector3 loc; //Местоположение спрайта на карте
    public bool flip = false; //Признак переворота спрайта по вертикали
    public float scale = 1f; //Масштаб спрайта
}

[System.Serializable]
public class CardDefinition
{
    //Этот класс хранит информацию о достоинстве карты
    public string face; //Спрайт, изображающий лицевую сторону карты
    public int rank; //Достоинство карты (1-13)
    public List<Decorator> pips = new List<Decorator> (); //Значки
}
