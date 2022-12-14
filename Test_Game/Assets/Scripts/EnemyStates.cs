using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    [SerializeField] public int _damage = 20;
    [SerializeField] public float _attackSpeed = 2f;
    [SerializeField] public float _health = 100;

    private void Start()
    {
        //if (PlayerPrefs.HasKey("damageE"))
        //{
        //    PlayerPrefs.GetInt("damageE");
        //    PlayerPrefs.GetInt("attackSpeedE");
        //    PlayerPrefs.GetInt("healthE");
        //}
        //else
        // {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("damageE", _damage);
        PlayerPrefs.SetFloat("attackSpeedE", _attackSpeed);
        PlayerPrefs.SetFloat("healthE", _health);
        // }

    }
}
