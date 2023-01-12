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

    //InitDeck вызываетс€ экземпл€ром Prospector, когда будет готов
    public void InitDeck(string deckXMLText)
    {
        ReadDeck(deckXMLText);
    }

    //ReadDeck читает указанный XML-файл и создает массив экземпл€ро
    //CardDefinition
    public void ReadDeck(string deckXMLText)
    {
        xmlr = new PT_XMLReader(); //—оздать новый экземпл€р PT_XMLReader
        xmlr.Parse(deckXMLText); //»спользовать его дл€ чтени€ DeckXML

        //¬ывод проверочной строки, чтобы показать, как использовать xmlr
        string s = "xml[0] decorator[0] ";
        s += "type=" + xmlr.xml["xml"][0]["decorator"][0].att("type");
        s += " x=" + xmlr.xml["xml"][0]["decorator"][0].att("x");
        s += " y=" + xmlr.xml["xml"][0]["decorator"][0].att("y");
        s += " scale=" + xmlr.xml["xml"][0]["decorator"][0].att("scale");

        //print(s);

        //ѕрочитать элементы <decorator> дл€ всех карт
        decorators = new List<Decorator>(); //»нициализировать список
                                            //экземпл€ров Decorator
                                            //»звлечь списое PT_XMLHashList всех элементов <decorator> из XML-файла
        PT_XMLHashList xDecos = xmlr.xml["xml"][0]["decorator"];
        Decorator deco;
        for(int i =0; i<xDecos.Count; i++)
        {
            //ƒл€ каждого элемента <decorator> в XML
            deco = new Decorator(); //—оздать экземпл€р Decorator
            //—копировать атрибуты из <decorator> в Decorator
            deco.type = xDecos[i].att("type");

        }

    }



}
