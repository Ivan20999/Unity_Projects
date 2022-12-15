using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ASPDEnhance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _textCurrent;
    private float _value;

    public void SetEnhance()
    {
        _value += 1;
        _textCurrent.text = _value.ToString();
        PlayerPrefs.SetFloat("attackSpeedP", _value);

    }

    void Start()
    {
        _text.text = "ASPD";
        _value = PlayerPrefs.GetFloat("attackSpeedP");
        _textCurrent.text = _value.ToString();
    }

    
}
