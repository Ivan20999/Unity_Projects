using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [Header("Set Dynamically")]
    public PT_XMLReader xmlr;
    public List<string> cardNames;
    public List<Card> cards;
    public List<Decorator> decorators;
    public List<CardDefinition> cardDefs;
    public Transform deckAnchor;
    public Dictionary<string, Sprite> dictSuits;

    //InitDeck ���������� ����������� Prospector, ����� ����� �����
    public void InitDeck(string deckXMLText)
    {
        ReadDeck(deckXMLText);
    }

    //ReadDeck ������ ��������� XML-���� � ������� ������ ����������
    //CardDefinition
    public void ReadDeck(string deckXMLText)
    {
        xmlr = new PT_XMLReader(); //������� ����� ��������� PT_XMLReader
        xmlr.Parse(deckXMLText); //������������ ��� ��� ������ DeckXML

        //����� ����������� ������, ����� ��������, ��� ������������ xmlr
        string s = "xml[0] decorator[0] ";
        s += "type=" + xmlr.xml["xml"][0]["decorator"][0].att("type");
        s += " x=" + xmlr.xml["xml"][0]["decorator"][0].att("x");
        s += " y=" + xmlr.xml["xml"][0]["decorator"][0].att("y");
        s += " scale=" + xmlr.xml["xml"][0]["decorator"][0].att("scale");

        //print(s);

        //��������� �������� <decorator> ��� ���� ����
        decorators = new List<Decorator>(); //���������������� ������
                                            //����������� Decorator
                                            //������� ������ PT_XMLHashList ���� ��������� <decorator> �� XML-�����
        PT_XMLHashList xDecos = xmlr.xml["xml"][0]["decorator"];
        Decorator deco;
        for(int i =0; i<xDecos.Count; i++)
        {
            //��� ������� �������� <decorator> � XML
            deco = new Decorator(); //������� ��������� Decorator
            //����������� �������� �� <decorator> � Decorator
            deco.type = xDecos[i].att("type");

        }

    }



}
