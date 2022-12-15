using TMPro;
using UnityEngine;

public class ATKEnhance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _textCurrent;
    private int _value;
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
            PlayerPrefs.SetInt("damageP", _value);
        }
    }

    void Start()
    {
        _text.text = "ATK";
        _value = PlayerPrefs.GetInt("damageP");
        _textCurrent.text = _value.ToString();
    }


}
