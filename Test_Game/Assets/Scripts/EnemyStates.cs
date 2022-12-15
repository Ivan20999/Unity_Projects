using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    [SerializeField] public int _damage = 20;
    [SerializeField] public float _attackSpeed = 2f;
    [SerializeField] public float _health = 100;

    private void Start()
    {
        if (PlayerPrefs.HasKey("damageE"))
        {
            PlayerPrefs.GetInt("damageE");
            PlayerPrefs.GetFloat("attackSpeedE");
            PlayerPrefs.GetFloat("healthE");
        }
        else
        {
            PlayerPrefs.SetInt("damageE", _damage);
            PlayerPrefs.SetFloat("attackSpeedE", _attackSpeed);
            PlayerPrefs.SetFloat("healthE", _health);

            Debug.Log(PlayerPrefs.GetInt("damageE"));
            Debug.Log(PlayerPrefs.GetFloat("attackSpeedE"));
            Debug.Log(PlayerPrefs.GetFloat("healthE"));
        }

    }
}
