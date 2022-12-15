using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    private int _coins;
    [SerializeField] TextMeshProUGUI _text;

    private void Start()
    {
        _coins = PlayerPrefs.GetInt("coins");
        _text.text = _coins.ToString();
    }

    public void AddOne()
    {
        _coins += 10;
        PlayerPrefs.SetInt("coins", _coins);
    }

    public void Update()
    {
        _coins = PlayerPrefs.GetInt("coins");
        _text.text = _coins.ToString();
    }

}

