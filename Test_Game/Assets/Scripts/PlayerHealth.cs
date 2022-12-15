using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health stats")]
    [SerializeField] public string _nameTag;
    [SerializeField] HealthBar _barHP;

    private float _currentHealth;
    private float _maxHealth = 100;

    private void Start()
    {
        _maxHealth = PlayerPrefs.GetFloat("healthP");
        _barHP.GetComponent<HealthBar>();
        _currentHealth = _maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == _nameTag)
        {
            ChangedHealth(-PlayerPrefs.GetInt("damageE"));
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
    }
}
