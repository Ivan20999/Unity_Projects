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
            _sprite.color = highlightColor; //������� ���� ������ ��� ��������� �� ��� ���������� ����.
        }
    }

    public void OnMouseExit()
    {
        if(_sprite != null)
        {
            _sprite.color = Color.white; //������� ���� ������ ��� �������� � ��� ���������� ����.
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f); //� ������ ������ ������ ������ ������ �������������
    }

    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if( _targetObject != null)
        {
            _targetObject.SendMessage(_targetMessage);//�������� ��������� �������� ������� � ������ ������ �� ������
        }
    }

}
