using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _attackSpeed = 4f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private int _coins = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("damageP"))
        {
            PlayerPrefs.GetInt("damageP");
            PlayerPrefs.GetInt("coins");
            PlayerPrefs.GetFloat("attackSpeedP");
            PlayerPrefs.GetFloat("healthP");
        }
        else
        {
            PlayerPrefs.SetInt("coins", _coins);
            PlayerPrefs.SetInt("damageP", _damage);
            PlayerPrefs.SetFloat("attackSpeedP", _attackSpeed);
            PlayerPrefs.SetFloat("healthP", _health);
        }
    }
}
