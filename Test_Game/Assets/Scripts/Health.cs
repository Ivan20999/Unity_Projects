using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health stats")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] public string _nameTag;
    [SerializeField] HealthBar _barHP;
    GameObject _coinManager;
    private bool _isEnemy;
    private int _currentHealth;

    //public event Action<float> HealthChanged;

    private void Start()
    {
        _barHP.GetComponent<HealthBar>();
        _currentHealth = _maxHealth;
        _coinManager = GameObject.FindGameObjectWithTag("CoinManager");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == _nameTag)
        {
            GameObject enteredObject = other.gameObject;
            ChangedHealth(-enteredObject.GetComponent<Damage>()._damage);
            _isEnemy = true;
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
            //HealthChanged?.Invoke(_currentHealthAsPercanLage);
            _barHP.OnHealthChange(bar, value);
        }
    }

    private void Death()
    {
        //HealthChanged?.Invoke(0);
        Destroy(this.gameObject);

        if (_isEnemy)
        {
            _coinManager.GetComponent<CoinManager>().AddOne();
        }

    }
}
