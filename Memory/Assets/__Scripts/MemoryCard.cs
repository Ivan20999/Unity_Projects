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

    //��� ������� ���������� ����� ����� �� �������
    public void OnMouseDown()
    {
        ////��������� ��� �����������, ���� ������ � ������ ������ 
        ////�������� ��������/�������.
        //if (_cardBack.activeSelf)
        //{
        //    //������ ������ ����������/���������
        //    _cardBack.SetActive(false);
        //}

        //�������� �������� canReveal �����������, ����������� �������������,
        //��� ������������ ����� ���� ������� ����� ��� �����.
        if(_cardBack.activeSelf && _controller.canReveal)
        {
            _cardBack.SetActive(false);
            _controller.CardRevealed(this); //����������� ����������� ��� �������� ���� �����.
        }
    }

    //�������� �����, ����������� ���������� SceneController ����� ������ �����
    //(������ �� ����� ������ card_back).
    public void Unreveal()
    {
        _cardBack.SetActive(true);
    }

    

}
