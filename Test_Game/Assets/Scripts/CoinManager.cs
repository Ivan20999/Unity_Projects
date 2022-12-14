using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int NumberOfCoins;
    [SerializeField] TextMeshProUGUI _text;

    private void Start()
    {
        _text.text = NumberOfCoins.ToString();
    }

    public void AddOne()
    {
        NumberOfCoins += 10;
        _text.text = NumberOfCoins.ToString();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            AddOne();
        }
    }
}

