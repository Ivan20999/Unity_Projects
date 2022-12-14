using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health stats")]
    [SerializeField] public string _nameTag;
    [SerializeField] HealthBar _barHP;

    private int _currentHealth;

    private void Start()
    {
        _barHP.GetComponent<HealthBar>();
        _currentHealth = PlayerPrefs.GetInt("healthP");
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
            float bar = (float)_currentHealth / PlayerPrefs.GetInt("healthP");
            _barHP.OnHealthChange(bar, value);
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
