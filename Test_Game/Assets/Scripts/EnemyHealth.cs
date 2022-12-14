using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health stats")]
    [SerializeField] public string _nameTag;
    [SerializeField] HealthBar _barHP;
    GameObject _coinManager;

    private int _currentHealth;

    private void Start()
    {
        _barHP.GetComponent<HealthBar>();
        _currentHealth = PlayerPrefs.GetInt("healthE");
        _coinManager = GameObject.FindGameObjectWithTag("CoinManager");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == _nameTag)
        {
            ChangedHealth(-PlayerPrefs.GetInt("damageP"));
        }
    }

    private void ChangedHealth(int value)
    {
        _currentHealth += value;

        if (_currentHealth <= 0)
        {
            Death();
        }
        else
        {
            float bar = (float)_currentHealth / PlayerPrefs.GetInt("healthE");
            _barHP.OnHealthChange(bar, value);
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
        _coinManager.GetComponent<CoinManager>().AddOne();
    }
}
