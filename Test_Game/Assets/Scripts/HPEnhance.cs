using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPEnhance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _textCurrent;
    private float _value;

    public void SetEnhance()
    {
        _value += 1;
        _textCurrent.text = _value.ToString();
        PlayerPrefs.SetFloat("healthP", _value);

    }

    void Start()
    {
        _text.text = "HP";
        _value = PlayerPrefs.GetFloat("healthP");
        _textCurrent.text = _value.ToString();
    }

    
}
