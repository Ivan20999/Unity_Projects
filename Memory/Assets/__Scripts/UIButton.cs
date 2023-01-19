using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private string _targetMessage;
    public Color highlightColor = Color.cyan;
    private SpriteRenderer _sprite = null;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>() as SpriteRenderer;
    }

    public void OnMouseOver()
    {
        if (_sprite != null)
        {
            _sprite.color = highlightColor; //Меняеем цвет кнопки при наведении на нее указателем мыши.
        }
    }

    public void OnMouseExit()
    {
        if(_sprite != null)
        {
            _sprite.color = Color.white; //Меняеем цвет кнопки при убирании с нее указателем мыши.
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f); //В момент щелчка размер кнопки слегка увеличивается
    }

    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if( _targetObject != null)
        {
            _targetObject.SendMessage(_targetMessage);//Отправка сообщения целевому обьекту в момент щелчка на кнопке
        }
    }

}
