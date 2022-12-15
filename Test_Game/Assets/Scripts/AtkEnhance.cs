using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ATKEnhance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _textCurrent;
    private int _value;

    public void SetEnhance()
    {
        _value += 1;
        _textCurrent.text = _value.ToString();
        PlayerPrefs.SetInt("damageP", _value);

    }

    void Start()
    {
        _text.text = "ATK";
        _value = PlayerPrefs.GetInt("damageP");
        _textCurrent.text = _value.ToString();
    }

    
}
