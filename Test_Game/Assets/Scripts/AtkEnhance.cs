using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AtkEnhance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textAtk;
    [SerializeField] TextMeshProUGUI _textAtkCurrent;
    private int _damage;

    public void SetEnhance()
    {
        _damage += 1;
        _textAtkCurrent.text = _damage.ToString();
        PlayerPrefs.SetInt("damageP", _damage);

    }

    void Start()
    {
        _textAtk.text = "ATK";
        _damage = PlayerPrefs.GetInt("damageP");
        _textAtkCurrent.text = _damage.ToString();
    }

    
}
