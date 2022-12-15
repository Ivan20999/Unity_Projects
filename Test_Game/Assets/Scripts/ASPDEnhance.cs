using TMPro;
using UnityEngine;

public class ASPDEnhance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _textCurrent;
    private float _value;
    private int _coins;
    private int _cost = 20;

    public void SetEnhance()
    {
        _coins = PlayerPrefs.GetInt("coins");
        if (_coins >= _cost)
        {
            _coins -= _cost;
            PlayerPrefs.SetInt("coins", _coins);

            _value += 1;
            _textCurrent.text = _value.ToString();
            PlayerPrefs.SetFloat("attackSpeedP", _value);
        }
    }

    void Start()
    {
        _text.text = "ASPD";
        _value = PlayerPrefs.GetFloat("attackSpeedP");
        _textCurrent.text = _value.ToString();
    }


}
