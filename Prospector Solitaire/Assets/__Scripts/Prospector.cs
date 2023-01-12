using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Класс управляет всей игрой
/// </summary>
public class Prospector : MonoBehaviour
{
    static public Prospector S;

    [Header("Set in Inspector")]
    public TextAsset deckXML;

    [Header("Set Dynamically")]
    public Deck deck;

    private void Awake()
    {
        S = this; //Подготовка обьекта-одиночки Prospector
    }

    private void Start()
    {
        deck = GetComponent<Deck>();// Получить компонент Deck
        deck.InitDeck(deckXML.text);// Передать ему DeckXML
    }
}
