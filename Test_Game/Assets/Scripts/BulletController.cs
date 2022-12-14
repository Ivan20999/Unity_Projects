using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject _enemy;
    private GameObject _player;
    public float _speedMove = 0.5f;

    public BulletState _state;


    public enum BulletState
    {
        start,//старт
        end   //попадание
    }

    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player");
        _speedMove = PlayerPrefs.GetInt("attackSpeedP"); 
        Debug.Log(_speedMove);
    }

    void Update()
    {
        if (_enemy != null)
        {
            switch (_state)
            {
                case BulletState.start:
                    gameObject.transform.Translate(Vector3.up * _speedMove * Time.deltaTime);

                    Vector3 TargetVector = _enemy.transform.position - gameObject.transform.position;

                    gameObject.transform.up = Vector3.Slerp(gameObject.transform.up, TargetVector, _speedMove * Time.deltaTime);

                    if (TargetVector.magnitude < 1)
                    {
                        _state = BulletState.end;
                    }

                    break;

                case BulletState.end:

                    Destroy(gameObject);

                    break;
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
