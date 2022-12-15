using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject _enemy;

    public float _speedMove = 4f;

    public BulletState _state;


    public enum BulletState
    {
        start,//старт
        end   //попадание
    }

    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");

        _speedMove = PlayerPrefs.GetFloat("attackSpeedP");

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
