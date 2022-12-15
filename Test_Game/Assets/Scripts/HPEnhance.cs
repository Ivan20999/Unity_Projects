using TMPro;
using UnityEngine;

public class HPEnhance : MonoBehaviour
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
            PlayerPrefs.SetFloat("healthP", _value);
        }
    }

    void Start()
    {
        _text.text = "HP";
        _value = PlayerPrefs.GetFloat("healthP");
        _textCurrent.text = _value.ToString();
    }


}
