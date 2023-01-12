using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //����� ��������� �����

 
    
}

[System.Serializable] //������������� ����� �������� ��� ������ � ����������
public class Decorator
{
    //���� ����� ������ ���������� �� DeckXML � ������ ������ �� �����
    public string type; //������, ������������ ����������� �����, �����
                        //type = "pip"
    public Vector3 loc; //�������������� ������� �� �����
    public bool flip = false; //������� ���������� ������� �� ���������
    public float scale = 1f; //������� �������
}

[System.Serializable]
public class CardDefinition
{
    //���� ����� ������ ���������� � ����������� �����
    public string face; //������, ������������ ������� ������� �����
    public int rank; //����������� ����� (1-13)
    public List<Decorator> pips = new List<Decorator> (); //������
}
