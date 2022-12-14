using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    [SerializeField] public int _damage = 20;
    [SerializeField] public float _attackSpeed = 2f;
    [SerializeField] public float _health = 100;
    [SerializeField] public int _coins = 0;

    private void Start()
    {
        //if (PlayerPrefs.HasKey("damageP"))
        //{
        //    PlayerPrefs.GetInt("damageP");
        //    PlayerPrefs.GetInt("attackSpeedP");
        //    PlayerPrefs.GetInt("healthP");
        //}
        //else
        // {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("damageP", _damage);
        PlayerPrefs.SetFloat("attackSpeedP", _attackSpeed);
        PlayerPrefs.SetFloat("healthP", _health);
        // }

    }
}
