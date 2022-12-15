using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health stats")]
    [SerializeField] public string _nameTag;
    [SerializeField] HealthBar _barHP;
    GameObject _coinManager;

    private float _currentHealth;
    private float _maxHealth = 100;

    private void Start()
    {
        _barHP.GetComponent<HealthBar>();
        _currentHealth = PlayerPrefs.GetFloat("healthE");
        _coinManager = GameObject.FindGameObjectWithTag("CoinManager");
        _currentHealth = _maxHealth;
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
            float bar = (float)_currentHealth / _maxHealth;
            _barHP.OnHealthChange(bar, value);
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
        _coinManager.GetComponent<CoinManager>().AddOne();
    }
}
