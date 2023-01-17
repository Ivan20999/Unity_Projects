using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject _cardBack;
    [SerializeField] private SceneController _controller;

    private int _id;

    public int id
    {
        get { return _id; }
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    //Эта функция вызывается после щелка на обьекте
    public void OnMouseDown()
    {
        ////Запускаем код деактивации, если обьект в данный момент 
        ////является активным/видимым.
        //if (_cardBack.activeSelf)
        //{
        //    //Делаем обьект неактивным/невидимым
        //    _cardBack.SetActive(false);
        //}

        //Проверка свойства canReveal контроллера, позволяющая гарантировать,
        //что одновременно могут быть открыты всего две карты.
        if(_cardBack.activeSelf && _controller.canReveal)
        {
            _cardBack.SetActive(false);
            _controller.CardRevealed(this); //уведомление контроллера при открытии этой карты.
        }
    }

    //Открытый метод, позволяющий компоненту SceneController снова скрыть карту
    //(вернув на често спрайт card_back).
    public void Unreveal()
    {
        _cardBack.SetActive(true);
    }

    

}
